using DataBase.Converters;
using domain.Repository;
using domain.Models;
using DataBase;


namespace Database.Repository;

public class UserRepository : IUserRepository
{
    private readonly ApplicationContext _context;

    public UserRepository(ApplicationContext context)
    {
        _context = context;
    }

    public bool Create(User item)
    {
        _context.Users.Add(item.ToDataBaseModel());
        return true;
    }

    public bool Delete(int id)
    {
        var user = GetItem(id);
        if (user == default)
            return false;

        _context.Users.Remove(user.ToDataBaseModel());
        return true;
    }

    public bool Update(User item)
    {
        _context.Users.Update(item.ToDataBaseModel());
        return true;
    }

    public IEnumerable<User> GetAll()
    {
        return _context.Users.Select(u => u.ToDomainModel());
    }

    public User? GetItem(int id)
    {
        var user = _context.Users.FirstOrDefault(u => u.Id == id);
        return user?.ToDomainModel();
    }

    public User? GetUserByLogin(int id)
    {
        var user = _context.Users.FirstOrDefault(u => u.Id == id);
        return user?.ToDomainModel();
    }

    public bool IsUserExists(int id)
    {
        return _context.Users.Any(u => u.Id == id);
    }

    public void Save()
    {
        _context.SaveChanges();
    }

    bool IUserRepository.CreateUser(User user)
    {
        throw new NotImplementedException();
    }

    User IRepository<User>.Get(int id)
    {
        throw new NotImplementedException();
    }

    User IUserRepository.GetUserByLogin(string login)
    {
        throw new NotImplementedException();
    }

    bool IUserRepository.IsUserExists(string login)
    {
        throw new NotImplementedException();
    }

    void IRepository<User>.Update(User item)
    {
        throw new NotImplementedException();
    }

    User? IUserRepository.GetUserById(int id)
    {
        throw new NotImplementedException();
    }

    bool IUserRepository.IsExists(int id)
    {
        throw new NotImplementedException();
    }

    bool IUserRepository.IsExists(string name)
    {
        throw new NotImplementedException();
    }
}
