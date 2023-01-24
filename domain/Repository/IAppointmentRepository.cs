using domain.Models;

namespace domain.Repository
{
    public interface IAppointmentRepository : IRepository<Appointment>
    {
        bool AddAppointment(Appointment appointment);
        List<Appointment> GetAppointmentBySpecialization(Specialization specialization);

        public IEnumerable<DateTime> GetFreeTime(Specialization spec);
        public IEnumerable<DateTime> GetFreeTime(int docid);


    }
}
