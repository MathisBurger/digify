using digify.AccessVoter;
using digify.Filters;
using digify.Models;
using digify.Models.Requests;
using digify.Modules;
using digify.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

    /// <summary>
    /// Creates a new class.
    /// </summary>
    [HttpPost("/class/createClass")]
    [TypeFilter(typeof(RequiresAuthorization))]
    public async Task<ActionResult<Class>> CreateClass([FromBody] CreateClass request)
    {
        if (!Authorization.IsGranted(AuthorizedUser, ClassVoter.CAN_CREATE, new ClassVoter(null, Db)))
        {
            return Unauthorized();
        }
        var newClass = new Class();
        newClass.Name = request.Name;
        foreach (var studentId in request.StudentsIDs)
        {
            var student = await Db.Users.Where(u => u.Id.ToString() == studentId).FirstOrDefaultAsync();
            if (student != null) newClass.Students.Add(student);
        }

        foreach (var teacherId in request.TeacherIDs)
        {
            var teacher = await Db.Users.Where(u => u.Id.ToString() == teacherId).FirstOrDefaultAsync();
            if (teacher != null) newClass.Teachers.Add(teacher);
        }

        Db.Classes.Add(newClass);
        await Db.SaveChangesAsync();
        return Ok(newClass);
    }
    
}