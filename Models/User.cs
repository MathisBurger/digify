using System.Text.Json.Serialization;

namespace digify.Models;

public class User: Entity
{

    public string Username { get; set; }
    [JsonIgnore]
    public string Password { get; set; }
    
    public string[] Roles { get; set; }
}