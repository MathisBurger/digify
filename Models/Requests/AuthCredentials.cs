using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace digify.Models.Requests;

/// <summary>
/// Creds used for login
/// </summary>
public class AuthCredentials
{
    /// <summary>
    /// The username
    /// </summary>
    [JsonPropertyName("login_name")]
    [Required]
    public string LoginName { get; set; }
    
    /// <summary>
    /// The password of the user
    /// </summary>
    [JsonPropertyName("password")]
    [Required]
    public string Password { get; set; }
}