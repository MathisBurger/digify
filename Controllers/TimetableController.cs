using digify.AccessVoter;
using digify.Filters;
using digify.Models;
using digify.Models.Responses;
using digify.Modules;
using digify.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    public async Task<ActionResult<Timetable>> GetTimetable()
    {
        var user = (await Db.Users.FindAsync(AuthorizedUser.Id))!;
        user.Timetable = (await Db.Timetables.Where(t => t.OwningUser.Id == user.Id).FirstOrDefaultAsync())!;
        if (user.Roles.Contains(UserRoles.STUDENT))
        {
            return Ok(await (new TimetableResponse(Db).FetchTimetable(user.Timetable!)));
        }

        return Unauthorized();
    }
    
}