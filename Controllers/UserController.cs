using digify.AccessVoter;
using digify.Filters;
using digify.Models;
using digify.Models.Requests;
using digify.Modules;
using digify.Shared;
using Microsoft.AspNetCore.Mvc;

namespace digify.Controllers;

[ApiController]
public class UserController : AuthorizedControllerBase
{
    private readonly IContext Db;
    private readonly IPasswordHasher Hasher;
    private readonly IAuthorization Authorization;

    public UserController(IAuthorization authorization, IContext _db, IPasswordHasher hasher)
    {
        Authorization = authorization;
        Db = _db;
        Hasher = hasher;
    }


    [HttpGet("/user/me")]
    [TypeFilter(typeof(RequiresAuthorization))]
    public ActionResult<User> Me() => Ok(AuthorizedUser);

    [HttpPost("/user/create")]
    [TypeFilter(typeof(RequiresAuthorization))]
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

    [HttpGet("/user/allUsers")]
    [TypeFilter(typeof(RequiresAuthorization))]
    public ActionResult<List<User>> AllUsers()
    {
        if (!Authorization.IsGranted(AuthorizedUser, UserVoter.ALL_USERS, new UserVoter()))
        {
            return Unauthorized();
        }

        var users = Db.Users.Select(x => x).ToList();
        return Ok(users);
    }
}