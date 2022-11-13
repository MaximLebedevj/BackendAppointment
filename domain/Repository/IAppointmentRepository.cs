using domain.Models;

namespace domain.Repository
{
    public interface IAppointmentRepository : IRepository<Appointment>
    {
        bool AddAppointment(Appointment appointment);
        List<Appointment> GetAppointmentBySpecialization(Specialization specialization);


    }
}
