using digify.Models;
using Microsoft.AspNetCore.Mvc;

namespace digify.Controllers;

[ApiController]
public class UserController : AuthorizedControllerBase
{

    [HttpGet("/api/user/me")]
    public ActionResult<User> Me() => Ok(AuthorizedUser);
}