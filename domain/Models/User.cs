
using domain.UseCases;

namespace domain.Models
{
    public class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public Role Role { get; set; }
        public User() : this("NULL", "NULL", 0, "NULL", "NULL", Role.Patient) { }
        public User(string login, string password, int id, string phoneNumber, string fullName, Role role)
        {
            Login = login;
            Password = password;
            Id = id;
            PhoneNumber = phoneNumber;
            FullName = fullName;
            Role = role;
        }

        public Result IsValid()
        {
            if (string.IsNullOrEmpty(Login))
                return Result.Fail("Invalid Login");

            if (string.IsNullOrEmpty(Password))
                return Result.Fail("Invalid Password");

            if (string.IsNullOrEmpty(PhoneNumber))
                return Result.Fail("Invalid PhoneNumber");

            if (string.IsNullOrEmpty(FullName))
                return Result.Fail("Invalid FullName");

            return Result.Ok();

        }
    }
}
