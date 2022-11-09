using domain.Models;

namespace domain.Repository
{
    public interface IDoctorRepository : IRepository<Doctor>
    {
        bool CreateDoctor(Doctor doctor);
        bool DeleteDoctor(int id);
        IEnumerable<Doctor> GetAllDoctors();
        Doctor GetDoctor(int id);
        Doctor GetDoctor(Specialization specialization);
    }
}
