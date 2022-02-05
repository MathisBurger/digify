using digify.Models;
using digify.Modules;
using digify.Shared;
using Microsoft.AspNetCore.Mvc;

namespace digify.Controllers;

/// <summary>
/// Abstract controller base used for handling requests
/// that needs authorization
/// </summary>
public class AuthorizedControllerBase : ControllerBase
{

    /// <summary>
    /// The AuthClaims of the user
    /// </summary>
    public AuthClaims AuthClaims { get; set; }
    
    /// <summary>
    /// The user that is currently logged in
    /// </summary>
    public User AuthorizedUser { get; set; }
}