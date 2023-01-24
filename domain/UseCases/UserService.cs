using domain.Models;
using domain.Repository;

namespace domain.UseCases
{
    public class UserService
    {
        private readonly IUserRepository _db;

        public UserService(IUserRepository db)
        {
            _db = db;
        }
        public Result<bool> IsUserExists(string login)
        {
            if (string.IsNullOrEmpty(login))
                return Result.Fail<bool>("Invalid login");

            return Result.Ok<bool>(_db.IsUserExists(login));
        }

        public Result<User> CreateUser(User user)
        {
            if (user.IsValid().IsFailure)
                return Result.Fail<User>(user.IsValid().Error);

            if (_db.IsUserExists(user.Login))
                return Result.Fail<User>("User already exists");

            bool success = _db.CreateUser(user);
            return success ? Result.Ok(user) : Result.Fail<User>("User has not been created");
        }

        public Result<User> GetUserByLogin(string login)
        {
            if (string.IsNullOrEmpty(login))
                return Result.Fail<User>("Invalid login");

            if (!_db.IsUserExists(login))
                return Result.Fail<User>("User doesn't exist");

            var user = _db.GetUserByLogin(login);
            return user is null ? Result.Fail<User>("User not found") : Result.Ok(user);
        }

        public Result<User> GetUserById(int id)
        {
            if (id < 0)
                return Result.Fail<User>("Invalid Id");
            var user = _db.GetUserById(id);
            if (user != null)
                return Result.Ok(user);
            return Result.Fail<User>("User Not Found");
        }

        public Result<bool> IsExists(int id)
        {
            if (id < 0)
                return Result.Fail<bool>("Invalid Id");

            return Result.Ok(_db.IsExists(id));
        }

        public Result<bool> IsExists(string name)
        {
            if (name == String.Empty)
                return Result.Fail<bool>("Please fill your name");

            return Result.Ok(_db.IsExists(name));
        }
        public Result<IEnumerable<User>> GetAll()
        {
            return Result.Ok(_db.GetAll());
        }
    }
}
