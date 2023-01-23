using DataBase.Converters;
using domain.Repository;
using domain.Models;
using DataBase;

namespace Database.Repository;

public class ScheduleRepository : IScheduleRepository
{
    private readonly ApplicationContext _context;

    public ScheduleRepository(ApplicationContext context)
    {
        _context = context;
    }

    public bool Create(Schedule item)
    {
        _context.Schedules.Add(item.ToDataBaseModel());
        return true;
    }

    public bool Delete(int id)
    {
        var sched = GetItem(id);
        if (sched == default)
            return false;

        _context.Schedules.Remove(sched.ToDataBaseModel());
        return true;
    }

    public bool EditSchedule(Schedule schedule)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Schedule> GetAll()
    {
        return _context.Schedules.Select(s => s.ToDomainModel());
    }

    public Schedule? GetItem(int id)
    {
        return _context.Schedules.FirstOrDefault(s => s.Id == id)?.ToDomainModel();
    }

    public IEnumerable<Schedule> GetScheduleOfDoctor(int doctorid)
    {
        return _context.Schedules.Where(s => s.DoctorId == doctorid).Select(s => s.ToDomainModel());
    }

    public void Save()
    {
        _context.SaveChanges();
    }

    public bool Update(Schedule item)
    {
        _context.Schedules.Update(item.ToDataBaseModel());
        return true;
    }

    bool IScheduleRepository.AddSchedule(Schedule schedule, Doctor doctor)
    {
        throw new NotImplementedException();
    }

    Schedule IRepository<Schedule>.Get(int id)
    {
        throw new NotImplementedException();
    }

    Schedule? IScheduleRepository.GetSchedule(int id)
    {
        throw new NotImplementedException();
    }

    List<Schedule> IScheduleRepository.GetScheduleOfDoctor(Doctor doctor)
    {
        throw new NotImplementedException();
    }

    void IRepository<Schedule>.Update(Schedule item)
    {
        throw new NotImplementedException();
    }

    bool IScheduleRepository.UpdateSchedule(Schedule schedule, Doctor doctor)
    {
        throw new NotImplementedException();
    }

    bool IScheduleRepository.UpdateSchedule(Schedule schedule)
    {
        throw new NotImplementedException();
    }
}