using domain.Models;
using domain.Repository;

namespace domain.UseCases
{
    public class AppointmentService
    {
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
    }
}
