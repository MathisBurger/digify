﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace digify.Models.Requests;

public class AuthCredentials
{
    [JsonPropertyName("login_name")]
    [Required]
    public string LoginName { get; set; }
    
    [JsonPropertyName("password")]
    [Required]
    public string Password { get; set; }

    private string loginName;
}