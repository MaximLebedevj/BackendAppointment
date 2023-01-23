using DataBase.Converters;
using domain.Repository;
using domain.Models;
using DataBase;

namespace Database.Repository;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly ApplicationContext _context;

    public AppointmentRepository(ApplicationContext context)
    {
        _context = context;
    }

    public bool Create(Appointment item)
    {
        _context.Appointments.Add(item.ToDataBaseModel());
        return true;
    }

    public bool Delete(int id)
    {
        var app = GetItem(id);
        if (app == default)
            return false;

        _context.Appointments.Remove(app.ToDataBaseModel());
        return true;
    }

    public IEnumerable<Appointment> GetAll()
    {
        return _context.Appointments.Select(a => a.ToDomainModel());
    }

    public IEnumerable<DateTime> GetFreeTime(int doctorId)
    {
        var doc = _context.Doctors.Where(d => d.Id == doctorId);
        return _context.Appointments.Where(a => doc.Any()).Select(a => a.EndTime);
    }

    public IEnumerable<DateTime> GetFreeTime(Specialization spec)
    {
        var docs = _context.Doctors.Where(d => d.Specialization == spec);
        return _context.Appointments.Where(a => docs.Any(d => d.Id == a.DoctorId)).Select(a => a.EndTime);
    }

    public Appointment? GetItem(int id)
    {
        return _context.Appointments.FirstOrDefault(a => a.Id == id)?.ToDomainModel();
    }

    public void Save()
    {
        _context.SaveChanges();
    }

    public bool Update(Appointment item)
    {
        _context.Appointments.Update(item.ToDataBaseModel());
        return true;
    }

    bool IAppointmentRepository.AddAppointment(Appointment appointment)
    {
        throw new NotImplementedException();
    }

    void IRepository<Appointment>.Create(Appointment item)
    {
        throw new NotImplementedException();
    }

    void IRepository<Appointment>.Delete(int id)
    {
        throw new NotImplementedException();
    }

    Appointment IRepository<Appointment>.Get(int id)
    {
        throw new NotImplementedException();
    }

    List<Appointment> IAppointmentRepository.GetAppointmentBySpecialization(Specialization specialization)
    {
        throw new NotImplementedException();
    }

    void IRepository<Appointment>.Update(Appointment item)
    {
        throw new NotImplementedException();
    }
}