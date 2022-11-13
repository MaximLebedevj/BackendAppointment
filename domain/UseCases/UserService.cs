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
    }
}
