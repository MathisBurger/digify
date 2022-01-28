using digify.Models;
using digify.Modules;
using Microsoft.AspNetCore.Mvc;

namespace digify.Controllers;

public class AuthorizedControllerBase : ControllerBase
{

    public IAuthorization Authorization { get; set; }

    public AuthClaims AuthClaims { get; set; }
    
    public User AuthorizedUser { get; set; }
}