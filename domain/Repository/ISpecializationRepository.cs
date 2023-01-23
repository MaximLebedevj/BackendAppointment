using domain.Models;

namespace domain.Repository;

public interface ISpecializationRepository : IRepository<Specialization>
{
    public Specialization? GetByName(string name);
}