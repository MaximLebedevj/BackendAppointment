using domain.Models;

namespace domain.Repository
{
    public interface IScheduleRepository : IRepository<Schedule>
    {
        public Schedule? GetSchedule(int id);
        List<Schedule> GetScheduleOfDoctor(Doctor doctor);
        bool AddSchedule(Schedule schedule, Doctor doctor);
        bool UpdateSchedule(Schedule schedule, Doctor doctor);
        public bool UpdateSchedule(Schedule schedule);
    }
}
