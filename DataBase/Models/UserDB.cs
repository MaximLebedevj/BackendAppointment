using domain.Models;

namespace DataBase.Models;

public class UserDB
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public Role Role { get; set; }
}
