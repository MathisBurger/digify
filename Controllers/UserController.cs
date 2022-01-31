using digify.AccessVoter;
using digify.Models;
using digify.Models.Requests;
using digify.Modules;
using digify.Shared;
using Microsoft.AspNetCore.Mvc;

namespace digify.Controllers;

[ApiController]
public class UserController : AuthorizedControllerBase
{
    private readonly DatabaseContext Db;
    private readonly IPasswordHasher Hasher;

    public UserController(IAuthorization authorization, DatabaseContext _db, IPasswordHasher hasher)
    {
        Authorization = authorization;
        Db = _db;
        Hasher = hasher;
    }


    [HttpGet("/user/me")]
    public ActionResult<User> Me() => Ok(AuthorizedUser);

    [HttpGet("/user/create")]
    public async Task<ActionResult<User>> CreateUser([FromBody] CreateUser request)
    {
        if (!Authorization.IsGranted(AuthorizedUser, UserVoter.CREATE_USER, new UserVoter()))
        {
            return Unauthorized();
        }

        var user = new User();
        user.Username = request.Username;
        user.Password = Hasher.HashFromPassword(request.Password);
        user.Roles = request.Roles;
        Db.Users.Add(user);
        await Db.SaveChangesAsync();
        return Ok(user);
    }
}