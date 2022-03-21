using digify.AccessVoter;
using digify.Filters;
using digify.Models;
using digify.Models.Requests;
using digify.Models.Responses;
using digify.Modules;
using digify.Services;
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
    private readonly TimetableService TimetableService;

    public TimetableController(IAuthorization authorization, IContext _db, IPasswordHasher hasher)
    {
        Authorization = authorization;
        Db = _db;
        Hasher = hasher;
        TimetableService = new TimetableService(Db);
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
        if (Authorization.IsGranted(user, TimetableVoter.READ, new TimetableVoter()))
        {
            return Ok(await (new TimetableResponse(Db).FetchTimetable(user.Timetable!)));
        }

        return Unauthorized();
    }

    [HttpGet("/timetable/actionGet")]
    [TypeFilter(typeof(RequiresAuthorization))]
    public async Task<ActionResult<Timetable>> GetTimetableForGroup(
        [FromQuery(Name = "user_id")] Guid? userId,
        [FromQuery(Name = "class_id")] Guid? classId
        ) {
        
        if (!Authorization.IsGranted(AuthorizedUser, TimetableVoter.ACTION_READ, new TimetableVoter()))
            return Unauthorized();
        
        if (userId != null)
        {
            var user = await Db.Users.FindAsync(userId);
            if (user == null) return BadRequest();
            var timetable = await Db.Timetables.Where(t => t.OwningUser.Id == user.Id).FirstOrDefaultAsync();
            return Ok(await new TimetableResponse(Db).FetchTimetable(timetable!));
        }

        if (classId != null)
        {
            var studentClass = await Db.Classes.FindAsync(classId);
            if (studentClass == null) return BadRequest("Class not found");
            var student = await Db.Users
                .Where(u => u.SchoolClass!.Id == studentClass.Id)
                .FirstOrDefaultAsync();
            if (student == null) return BadRequest("Class contains no students");
            var timetable = await Db.Timetables.Where(t => t.OwningUser.Id == student.Id).FirstOrDefaultAsync();
            if (timetable == null) return BadRequest("Timetable for class not found");
            return Ok(await new TimetableResponse(Db).FetchTimetable(timetable!));
        }

        return BadRequest();
    }

    [HttpPost("/timetable/create/forUser")]
    [TypeFilter(typeof(RequiresAuthorization))]
    public async Task<ActionResult<Timetable>> CreateTimetableForUser([FromBody] TimetableManagementRequest request)
    {
        if (
            !Authorization.IsGranted(AuthorizedUser, TimetableVoter.CREATE, new TimetableVoter())
            || request.UserId == null 
            || request.RequestTableElements == null
            ) {
            return Unauthorized();
        }

        var user = await Db.Users.FindAsync(request.UserId);
        if (user == null)
        {
            return BadRequest();
        }

        return Ok(
            await new TimetableResponse(Db).FetchTimetable(
                await TimetableService.CreateTimeTable(user, request.RequestTableElements)
                )
            );
    }

    [HttpDelete("/timetable/delete/forUser")]
    [TypeFilter(typeof(RequiresAuthorization))]
    public async Task<ActionResult> DeleteTimetableForUser([FromBody] TimetableManagementRequest request)
    {
        if (
            !Authorization.IsGranted(AuthorizedUser, TimetableVoter.DELETE, new TimetableVoter())
            || request.UserId == null
        ) {
            return Unauthorized();
        }

        var user = await Db.Users.FindAsync(request.UserId);
        if (user == null)
        {
            return BadRequest();
        }

        return Ok(
            await new TimetableResponse(Db).FetchTimetable(
                await TimetableService.DeleteTimeTable(user.Timetable!.Id)
                )
            );
    }

    [HttpPost("/timetable/update/forUser")]
    [TypeFilter(typeof(RequiresAuthorization))]
    public async Task<ActionResult<Timetable>> UpdateTimetableForUser([FromBody] TimetableManagementRequest request)
    {
        if (request.UserId == null || request.RequestTableElements == null)
        {
            return BadRequest();
        }

        if (!Authorization.IsGranted(AuthorizedUser, TimetableVoter.UPDATE, new TimetableVoter()))
            return Unauthorized();
        var user = await Db.Users.FindAsync(request.UserId);
        if (user == null)
        {
            return BadRequest();
        }
        var table = await TimetableService.UpdateTimetableForUser(user, request.RequestTableElements);
        return Ok(await new TimetableResponse(Db).FetchTimetable(table));
    }

    [HttpPost("/timetable/create/forClass")]
    [TypeFilter(typeof(RequiresAuthorization))]
    public async Task<ActionResult<Timetable>> CreateTimetableForClass([FromBody] TimetableManagementRequest request)
    {
        if (
            !Authorization.IsGranted(AuthorizedUser, TimetableVoter.UPDATE, new TimetableVoter())
            || request.ClassId == null 
            || request.RequestTableElements == null
        ) {
            return Unauthorized();
        }

        var fetchedClass = await Db.Classes.FindAsync(request.ClassId);
        if (fetchedClass == null)
        {
            return BadRequest();
        }

        return Ok(
            await new TimetableResponse(Db).FetchTimetable(
                await TimetableService.CreateTimetableForClass(fetchedClass, request.RequestTableElements)
            )
        );
    }
    
    [HttpDelete("/timetable/delete/forClass")]
    [TypeFilter(typeof(RequiresAuthorization))]
    public async Task<ActionResult> DeleteTimetableForClass([FromBody] TimetableManagementRequest request)
    {
        if (
            !Authorization.IsGranted(AuthorizedUser, TimetableVoter.UPDATE, new TimetableVoter())
            || request.ClassId == null
        ) {
            return Unauthorized();
        }

        var fetchedClass = await Db.Classes.FindAsync(request.ClassId);
        if (fetchedClass == null)
        {
            return BadRequest();
        }

        return Ok(
            await new TimetableResponse(Db).FetchTimetable(
                await TimetableService.DeleteTimetableForClass(fetchedClass)
            )
        );
    }
    
    [HttpPost("/timetable/update/forClass")]
    [TypeFilter(typeof(RequiresAuthorization))]
    public async Task<ActionResult> UpdateTimetableForClass([FromBody] TimetableManagementRequest request)
    {
        if (request.ClassId == null || request.RequestTableElements == null)
        {
            return BadRequest("Invalid request params");
        }
        if (!Authorization.IsGranted(AuthorizedUser, TimetableVoter.UPDATE, new TimetableVoter()))
            return Unauthorized();
        var fetchedClass = await Db.Classes.FindAsync(request.ClassId);
        if (fetchedClass == null)
        {
            return BadRequest("Class not found");
        }
        await TimetableService.UpdateTimetableForClass(fetchedClass, request.RequestTableElements);
        return Ok();
    }
}