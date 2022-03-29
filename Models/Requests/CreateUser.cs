using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace digify.Models.Requests;

/// <summary>
/// Request type used in user creation requests
/// </summary>
public class CreateUser
{
    /// <summary>
    /// The username of the user
    /// </summary>
    [JsonPropertyName("name")]
    [Required]
    public string Username { get; set; }
    
    /// <summary>
    /// The password of the user
    /// </summary>
    [JsonPropertyName("password")]
    [Required]
    public string Password { get; set; }
    
    /// <summary>
    /// All roles the user will have
    /// </summary>
    [JsonPropertyName("roles")]
    [Required]
    public string[] Roles { get; set; }
}