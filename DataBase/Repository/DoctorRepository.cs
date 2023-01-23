using DataBase.Converters;
using domain.Repository;
using domain.Models;
using DataBase;

namespace Database.Repository;

public class DoctorRepository : IDoctorRepository
{
    private readonly ApplicationContext _context;

    public DoctorRepository(ApplicationContext context)
    {
        _context = context;
    }

    public bool Create(Doctor item)
    {
        _context.Doctors.Add(item.ToDataBaseModel());
        return true;
    }

    public bool Delete(int id)
    {
        var doctor = GetItem(id);
        if (doctor == default)
            return false;

        _context.Remove(doctor.ToDataBaseModel());
        return true;
    }

    public Doctor? FindDoctor(int id)
    {
        var doctor = _context.Doctors.FirstOrDefault(d => d.Id == id);
        return doctor?.ToDomainModel();
    }

    public IEnumerable<Doctor> FindDoctor(Specialization spec)
    {
        return _context.Doctors.Where(d => d.Specialization == spec).Select(d => d.ToDomainModel());
    }

    public IEnumerable<Doctor> GetAll()
    {
        return _context.Doctors.Select(d => d.ToDomainModel());
    }

    public Doctor? GetItem(int id)
    {
        return _context.Doctors.FirstOrDefault(d => d.Id == id)?.ToDomainModel();
    }

    public bool IsExists(int id)
    {
        return _context.Doctors.Any(d => d.Id == id);
    }

    public void Save()
    {
        _context.SaveChanges();
    }

    public bool Update(Doctor item)
    {
        _context.Doctors.Update(item.ToDataBaseModel());
        return true;
    }

    void IRepository<Doctor>.Create(Doctor item)
    {
        throw new NotImplementedException();
    }

    bool IDoctorRepository.CreateDoctor(Doctor doctor)
    {
        throw new NotImplementedException();
    }

    void IRepository<Doctor>.Delete(int id)
    {
        throw new NotImplementedException();
    }

    bool IDoctorRepository.DeleteDoctor(int id)
    {
        throw new NotImplementedException();
    }

    Doctor IRepository<Doctor>.Get(int id)
    {
        throw new NotImplementedException();
    }

    IEnumerable<Doctor> IDoctorRepository.GetAllDoctors()
    {
        throw new NotImplementedException();
    }

    Doctor IDoctorRepository.GetDoctor(int id)
    {
        throw new NotImplementedException();
    }

    Doctor IDoctorRepository.GetDoctor(Specialization specialization)
    {
        throw new NotImplementedException();
    }

    void IRepository<Doctor>.Update(Doctor item)
    {
        throw new NotImplementedException();
    }
}
