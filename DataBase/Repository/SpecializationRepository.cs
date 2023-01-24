using DataBase.Converters;
using domain.Repository;
using domain.Models;
using DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Database.Repository;

public class SpecializationRepository : IRepository<Specialization>
{
    private readonly ApplicationContext _context;

    public SpecializationRepository(ApplicationContext context)
    {
        _context = context;
    }

    public bool Create(Specialization item)
    {
        _context.specializations.Add(item.ToDataBaseModel());
        return true;
    }

    public bool Delete(int id)
    {
        var spec = GetItem(id);
        if (spec == default)
            return false;

        _context.specializations.Remove(spec.ToDataBaseModel());
        return true;
    }

    public bool Update(Specialization item)
    {
        _context.specializations.Update(item.ToDataBaseModel());
        return true;
    }

    public IEnumerable<Specialization> GetAll()
    {
        return _context.specializations.Select(s => s.ToDomainModel());
    }

    public Specialization? GetItem(int id)
    {
        return _context.specializations.FirstOrDefault(s => s.Id == id)?.ToDomainModel();
    }

    public Specialization? GetById(int id)
    {
        return _context.specializations.FirstOrDefault(s => s.Id == id)?.ToDomainModel();
    }

    public void Save()
    {
        _context.SaveChanges();
    }

    IEnumerable<Specialization> IRepository<Specialization>.GetAll()
    {
        throw new NotImplementedException();
    }

    Specialization IRepository<Specialization>.Get(int id)
    {
        throw new NotImplementedException();
    }
    void IRepository<Specialization>.Update(Specialization item)
    {
        throw new NotImplementedException();
    }
}
