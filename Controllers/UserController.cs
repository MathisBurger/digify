using digify.AccessVoter;
using digify.Filters;
using digify.Models;
using digify.Models.Requests;
using digify.Modules;
using digify.Shared;
using Microsoft.AspNetCore.Mvc;

namespace digify.Controllers;

/// <summary>
/// Handles user specific requests
/// </summary>
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


    /// <summary>
    /// Returns the current logged in user
    /// </summary>
    [HttpGet("/api/user/me")]
    [TypeFilter(typeof(RequiresAuthorization))]
    public ActionResult<User> Me() => Ok(AuthorizedUser);

    /// <summary>
    /// Creates a new user.
    /// </summary>
    [HttpPost("/api/user/create")]
    [TypeFilter(typeof(RequiresAuthorization))]
    public async Task<ActionResult<User>> CreateUser([FromBody] CreateUser request)
    {
        if (!Authorization.IsGranted(AuthorizedUser, UserVoter.CREATE_USER, new UserVoter()))
        {
            return Unauthorized();
        }

        var user = new User();
        var timeTable = new Timetable();
        user.Username = request.Username;
        user.Password = Hasher.HashFromPassword(request.Password);
        user.Roles = request.Roles;
        user.Timetable = timeTable;
        Db.Timetables.Add(timeTable);
        Db.Users.Add(user);
        await Db.SaveChangesAsync();
        return Ok(user);
    }

    /// <summary>
    /// Fetches all users.
    /// </summary>
    [HttpGet("/api/user/allUsers")]
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

    /// <summary>
    /// Deletes a user from the system
    /// </summary>
    /// <param name="id">The ID of the user that should be deleted</param>
    [HttpDelete("/api/user/delete/{id}")]
    [TypeFilter(typeof(RequiresAuthorization))]
    public async Task<ActionResult> DeleteUser([FromRoute] Guid id)
    {
        if (!Authorization.IsGranted(AuthorizedUser, UserVoter.DELETE_USER, new UserVoter()))
        {
            return Unauthorized();
        }

        var user = await Db.Users.FindAsync(id);
        if (null == user)
        {
            return BadRequest();
        }

        Db.Remove(user);
        await Db.SaveChangesAsync();
        return Ok();
    }
}