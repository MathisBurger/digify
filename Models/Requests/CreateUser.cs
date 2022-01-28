using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace digify.Models.Requests;

public class CreateUser
{
    [JsonPropertyName("name")]
    [Required]
    public string Username { get; set; }
    
    [JsonPropertyName("password")]
    [Required]
    public string Password { get; set; }
    
    [JsonPropertyName("roles")]
    [Required]
    public string[] Roles { get; set; }
}