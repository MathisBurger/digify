using digify.AccessVoter;
using digify.Filters;
using digify.Models;
using digify.Modules;
using digify.Shared;
using Microsoft.AspNetCore.Mvc;

namespace digify.Controllers;

/// <summary>
/// Handling basic timetable operations
/// </summary>
[ApiController]
public class TimetableController : AuthorizedControllerBase
{
    
    private readonly IContext Db;
    private readonly IPasswordHasher Hasher;
    private readonly IAuthorization Authorization;

    public TimetableController(IAuthorization authorization, IContext _db, IPasswordHasher hasher)
    {
        Authorization = authorization;
        Db = _db;
        Hasher = hasher;
    }

    /// <summary>
    /// Gets the current timetable of the user
    /// </summary>
    /// <returns>The current timetable of the user</returns>
    [HttpGet("/timetable/get")]
    [TypeFilter(typeof(RequiresAuthorization))]
    public ActionResult<Timetable> GetTimetable()
    {
        if (AuthorizedUser.Roles.Contains(UserRoles.STUDENT))
        {
            return Ok(AuthorizedUser.Timetable ?? new Timetable());
        }

        return Unauthorized();
    }
    
}