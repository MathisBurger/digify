using digify.Filters;
using digify.Models;
using digify.Models.Responses;
using digify.Modules;
using digify.Services;
using digify.Shared;
using Microsoft.AspNetCore.Mvc;

namespace digify.Controllers;

/// <summary>
/// Handles all classbook user requests.
/// </summary>
[ApiController]
public class ClassbookController : AuthorizedControllerBase
{
    private readonly IContext Db;
    private readonly IAuthorization Authorization;
    private readonly ILogger<ClassController> Logger;
    private readonly ClassbookService ClassbookService;
    
    public ClassbookController(IContext _db, IAuthorization _auth, ILogger<ClassController> logger)
    {
        Db = _db;
        Authorization = _auth;
        Logger = logger;
        ClassbookService = new ClassbookService(_db, logger);
    }

    [HttpGet("/classbook")]
    [TypeFilter(typeof(RequiresAuthorization))]
    public async Task<ActionResult<Classbook>> GetClassbookOfCurrentUser()
    {
        if (!AuthorizedUser.Roles.Contains(UserRoles.STUDENT))
        {
            return BadRequest("You do not own any classbook, because you are not a member of a class");
        }

        return Ok(await new ClassbookResponse(Db).ParseSingle(await ClassbookService.GetClassbookByStudent(AuthorizedUser)));
    }
    
}