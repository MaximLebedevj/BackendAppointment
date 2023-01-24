using domain.Models;

namespace domain.Repository
{
    public interface IDoctorRepository : IRepository<Doctor>
    {
        public Doctor? FindDoctor(int id);
        public IEnumerable<Doctor> FindDoctor(Specialization spec);
        public bool IsExists(int id);
        bool CreateDoctor(Doctor doctor);
        bool DeleteDoctor(int id);
        IEnumerable<Doctor> GetAllDoctors();
        Doctor GetDoctor(int id);
        Doctor GetDoctor(Specialization specialization);
    }
}
