using digify.AccessVoter;
using digify.Filters;
using digify.Models;
using digify.Modules;
using digify.Shared;
using Microsoft.AspNetCore.Mvc;

namespace digify.Controllers;

/// <summary>
/// Handles class specific endpoint requests
/// </summary>
[ApiController]
public class ClassController : AuthorizedControllerBase
{
    private readonly IContext Db;
    private readonly IAuthorization Authorization;

    public ClassController(IContext _db, IAuthorization _auth)
    {
        Db = _db;
        Authorization = _auth;
    }

    /// <summary>
    /// Fetches all classes that the current logged
    /// in user has access to.
    /// </summary>
    [HttpGet("/class/getAllClasses")]
    [TypeFilter(typeof(RequiresAuthorization))]
    public ActionResult<List<Class>> GetAllClasses()
    {
        if (Authorization.IsGranted(AuthorizedUser, UserRoles.ADMIN, new UserVoter()))
        {
            return Db.Classes.Select(c => c).ToList();
        }
        // Check if user is teacher
        if (AuthorizedUser.schoolClass == null)
        {
            return Db.Classes.Where(c => c.Teachers.Contains(AuthorizedUser)).ToList();
        }

        return Db.Classes.Where(c => c.Students.Contains(AuthorizedUser)).ToList();
    }
    
}