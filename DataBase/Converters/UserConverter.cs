using domain.Models;
using DataBase.Models;

namespace DataBase.Converters;

public static class UserConverter
{
    public static UserDB ToDataBaseModel(this User user)
    {
        return new UserDB
        {
            Id = user.Id,
            Login = user.Login,
            Password = user.Password,
            FullName = user.FullName,
            PhoneNumber = user.PhoneNumber,
            Role = user.Role
        };
    }
    public static User ToDomainModel(this UserDB userDB)
    {
        return new User
        {
            Id = userDB.Id,
            Login = userDB.Login,
            Password = userDB.Password,
            FullName = userDB.FullName,
            PhoneNumber = userDB.PhoneNumber,
            Role = userDB.Role
        };
    }
}
