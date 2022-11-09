using domain.Models;

namespace domain.Repository
{
    public interface IScheduleRepository : IRepository<Schedule>
    {
        List<Schedule> GetScheduleOfDoctor(Doctor doctor);
        bool AddSchedule(Schedule schedule, Doctor doctor);
        bool UpdateSchedule(Schedule schedule, Doctor doctor);
    }
}
