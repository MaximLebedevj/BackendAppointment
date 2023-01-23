using domain.Models;
using System.Text.Json.Serialization;

namespace BackendAppointment.Views;

public class UserSearchView
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("phone number")]
    public string Tel { get; set; }
    [JsonPropertyName("full name")]
    public string Name { get; set; }
    [JsonPropertyName("role")]
    public Role Role { get; set; }


    [JsonPropertyName("username")]
    public string UserName { get; set; }
    [JsonPropertyName("password")]
    public string Password { get; set; }
}
