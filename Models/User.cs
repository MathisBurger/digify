using System.Text.Json.Serialization;

namespace digify.Models;

public class User: Entity
{
    public const string ROLE_ADMIN = "ADMIN";
    public const string ROLE_TEACHER = "TEACHER";
    public const string ROLE_STUDENT = "STUDENT";
    
    public string Username { get; set; }
    [JsonIgnore]
    public string Password { get; set; }
    
    public string[] Roles { get; set; }
}