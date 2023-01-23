using domain.Models;
using domain.Repository;

namespace domain.UseCases
{
    public class DoctorService
    {
        private readonly IDoctorRepository _db;
        private readonly IAppointmentRepository _appdb;
        private IDoctorRepository @object;

        public DoctorService(IDoctorRepository db, IAppointmentRepository appdb)
        {
            _db = db;
            _appdb = appdb;
        }

        public DoctorService(IDoctorRepository @object)
        {
            this.@object = @object;
        }

        public Result<Doctor> CreateDoctor(Doctor doctor)
        {
            if (doctor.IsValid().IsFailure)
                return Result.Fail<Doctor>(doctor.IsValid().Error);

            if (GetDoctor(doctor.Id).Success)
                return Result.Fail<Doctor>("doctor already exists");

            var success = _db.CreateDoctor(doctor);
            return success ? Result.Ok(doctor) : Result.Fail<Doctor>("Doctor has not been created");
        }

        public Result<Doctor> GetDoctor(int id)
        {
            var doctor = _db.GetDoctor (id);
            return doctor is not null ? Result.Ok(doctor) : Result.Fail<Doctor>("Doctor not found");
        }

        public Result<Doctor> GetDoctor (Specialization specialization)
        {
            var valid = specialization.IsValid();
            if (valid.IsFailure)
                return Result.Fail <Doctor>(valid.Error);

            var doctor = _db.GetDoctor(specialization);
            return doctor != null ? Result.Ok(doctor) : Result.Fail<Doctor>("Doctor not found");
        }

        public Result<Doctor> DeleteDoctor(int id)
        {
            if (_appdb.GetFreeTime(id).Any())
                return Result.Fail<Doctor>("Unable to delete doctor: Doctor has appointments");

            var result = FindDoctor(id);
            if (result.IsFailure)
                return Result.Fail<Doctor>(result.Error);

            if (_db.Delete(id))
            {
                _db.Save();
                return result;
            }
            return Result.Fail<Doctor>("Unable to delete doctor");
        }

        public Result<Doctor> DeleteDoctor (int id, IEnumerable<Appointment> appointments) 
        {
            if (appointments.Any())
                return Result.Fail<Doctor>("Cannot delete doctor with appointments");

            var doctor = GetDoctor(id);
            if (doctor.IsFailure)
                return Result.Fail<Doctor>("Doctor not found");

            return _db.DeleteDoctor(id) ? doctor : Result.Fail<Doctor>("User has not been deleted");
        }

        public Result<Doctor> FindDoctor(int id)
        {
            if (id < 0)
                return Result.Fail<Doctor>("Invalid Id");
            var doctor = _db.FindDoctor(id);
            if (doctor != null)
                return Result.Ok(doctor);
            return Result.Fail<Doctor>("Doctor not found");
        }

        public Result<IEnumerable<Doctor>> FindDoctor(Specialization spec)
        {
            var result = spec.IsValid();
            if (result.IsFailure)
                return Result.Fail<IEnumerable<Doctor>>("Invalid Spec");
            var doctor = _db.FindDoctor(spec);
            if (doctor != null)
                return Result.Ok(doctor);
            return Result.Fail<IEnumerable<Doctor>>("Doctor not found");
        }

        public Result<IEnumerable<Doctor>> GetAllDoctors()
        {
            return Result.Ok(_db.GetAllDoctors());
        }
    }
}
