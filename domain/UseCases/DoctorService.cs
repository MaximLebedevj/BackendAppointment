using domain.Models;
using domain.Repository;

namespace domain.UseCases
{
    public class DoctorService
    {
        private readonly IDoctorRepository _db;

        public DoctorService(IDoctorRepository db)
        {
            _db = db;
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

        public Result<Doctor> DeleteDoctor (int id, IEnumerable<Appointment> appointments) 
        {
            if (appointments.Any())
                return Result.Fail<Doctor>("Cannot delete doctor with appointments");

            var doctor = GetDoctor(id);
            if (doctor.IsFailure)
                return Result.Fail<Doctor>("Doctor not found");

            return _db.DeleteDoctor(id) ? doctor : Result.Fail<Doctor>("User has not been deleted");
        }

        public Result<IEnumerable<Doctor>> GetAllDoctors()
        {
            return Result.Ok(_db.GetAllDoctors());
        }
    }
}
