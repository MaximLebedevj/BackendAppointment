using domain.Models;
using domain.Repository;

namespace domain.UseCases
{
    public class AppointmentService
    {
        public readonly static Dictionary<int, Mutex> _mutexes = new();
        private readonly IAppointmentRepository _db;

        public AppointmentService(IAppointmentRepository db)
        {
            _db = db;
        }

        public Result<List<Appointment>> GetAppointmentBySpecialization(Specialization specialization)
        {
            var validSpecialization = specialization.IsValid();
            if (validSpecialization.IsFailure)
                return Result.Fail<List<Appointment>>("Invalid Specialization (" + validSpecialization.Error + ")");

            var result = _db.GetAppointmentBySpecialization(specialization);
            return result.Count > 0 ? Result.Ok(result) : Result.Fail<List<Appointment>>("Failed to get appointment");
        }

        public Result<Appointment> SaveAppointment(Appointment appointment, Schedule schedule)
        {
            var result = appointment.IsValid();
            if (result.IsFailure)
                return Result.Fail<Appointment>("Invalid appointment: " + result.Error);

            var result1 = schedule.IsValid();
            if (result1.IsFailure)
                return Result.Fail<Appointment>("Invalid schedule: " + result1.Error);

            if (schedule.StartWorkingDay > appointment.StartTime || schedule.EndWorkingDay < appointment.EndTime)
                return Result.Fail<Appointment>("Appointment out of schedule");

            var apps = _db.GetFreeTime(appointment.DoctorId);
            if (apps.Any(a => appointment.StartTime > a))
                return Result.Fail<Appointment>("Appointment time already taken");

            if (!_mutexes.ContainsKey(appointment.DoctorId))
            {
                _mutexes.Add(appointment.DoctorId, new Mutex());
            }
            _mutexes.First(d => d.Key == appointment.DoctorId).Value.WaitOne();

            if (_db.Create(appointment))
            {
                _db.Save();
                _mutexes.First(d => d.Key == appointment.DoctorId).Value.ReleaseMutex();
                return Result.Ok(appointment);
            }
            _mutexes.First(d => d.Key == appointment.DoctorId).Value.ReleaseMutex();
            return Result.Fail<Appointment>("Unable to save appointment");
        }



        public Result<Appointment> AddAnyAppointment(Specialization specialization)
        {
            var validSpecialization = specialization.IsValid();
            if (validSpecialization.IsFailure)
                return Result.Fail<Appointment>("Invalid Specialization (" + validSpecialization.Error + ")");

            var availableDoctors = GetAppointmentBySpecialization(specialization);
            if (availableDoctors.Value.Count < 1)
                return Result.Fail<Appointment>("No available doctors of this specialization");

            var result = AddConcreteDoctorAppointment(availableDoctors.Value[0]);
            return result.Success ? Result.Ok(availableDoctors.Value[0]) : Result.Fail<Appointment>("Appointment has not been added");
        }



        public Result<Appointment> AddConcreteDoctorAppointment(Appointment appointment)
        {
            var validAppointment = appointment.IsValid();
            if (validAppointment.IsFailure)
                return Result.Fail<Appointment>("Invalid Appointment (" + validAppointment.Error + ")");

            var success = _db.AddAppointment(appointment);
            return success ? Result.Ok(appointment) : Result.Fail<Appointment>("Appointment has not been added");
        }

        public Result<IEnumerable<DateTime>> GetFreeTime(Specialization spec)
        {
            var result = spec.IsValid();
            if (result.IsFailure)
                return Result.Fail<IEnumerable<DateTime>>("Invalid Spec");
            return Result.Ok(_db.GetFreeTime(spec));
        }

        public Result<IEnumerable<DateTime>> GetFreeTime(int docid)
        {
            if (docid < 0)
                return Result.Fail<IEnumerable<DateTime>>("Invalid Doctor ID");
            return Result.Ok(_db.GetFreeTime(docid));
        }
    }
}
