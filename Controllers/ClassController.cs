using digify.AccessVoter;
using digify.Filters;
using digify.Models;
using digify.Models.Requests;
using digify.Models.Responses;
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
    private readonly ILogger<ClassController> Logger;

    public ClassController(IContext _db, IAuthorization _auth, ILogger<ClassController> logger)
    {
        Db = _db;
        Authorization = _auth;
        Logger = logger;
    }

    /// <summary>
    /// Fetches all classes that the current logged
    /// in user has access to.
    /// </summary>
    [HttpGet("/class/getAllClasses")]
    [TypeFilter(typeof(RequiresAuthorization))]
    public async Task<ActionResult<List<Class>>> GetAllClasses()
    {
        if (Authorization.IsGranted(AuthorizedUser, UserRoles.ADMIN, new UserVoter()))
        {
            return Ok(await new ClassResponse(Db).ParseList(Db.Classes.ToList()));
        }
        // Check if user is teacher
        if (AuthorizedUser.SchoolClass == null)
        {
            return Ok(new List<Class>() {Db.Classes.ToArray()[0]});
        }

        return Ok(
            await new ClassResponse(Db).ParseList(
                Db.Classes.Where(c => c.Students.Contains(AuthorizedUser)).ToList()
                )
            );
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
            var student = await Db.Users.FindAsync(Guid.Parse(studentId));
            if (student != null)
            {
                student.SchoolClass = newClass;
                Db.Update(student);
            }
        }

        foreach (var teacherId in request.TeacherIDs)
        {
            var teacher = await Db.Users.FindAsync(Guid.Parse(teacherId));
            if (teacher != null)
            {
                var teacherClass = new TeacherClass();
                teacherClass.Class = newClass;
                teacherClass.Teacher = teacher;
                Db.Add(teacherClass);
            }
        }

        Db.Add(newClass);
        await Db.SaveChangesAsync();
        Logger.LogInformation("Created class");
        return Ok(newClass);
    }
    
}