using domain.Models;

namespace domain.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        bool IsUserExists(string login);
        bool CreateUser(User user);
        User GetUserByLogin(string login);
        public User? GetUserById(int id);
        public bool IsExists(int id);
        public bool IsExists(string name);
    }
}
