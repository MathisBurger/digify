using digify.Models;
using digify.Modules;
using digify.Shared;
using Microsoft.AspNetCore.Mvc;

namespace digify.Controllers;

public class AuthorizedControllerBase : ControllerBase
{

    public AuthClaims AuthClaims { get; set; }
    
    public User AuthorizedUser { get; set; }
}