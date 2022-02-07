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
            return Ok(Db.Classes.ToList());
        }
        // Check if user is teacher
        if (AuthorizedUser.SchoolClass == null)
        {
            return Db.Classes
                .Where(c => 
                    c.Teachers.Where(t => t.TeacherId == AuthorizedUser.Id).FirstOrDefault() != null
                    ).ToList();
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
            if (student != null)
            {
                student.SchoolClassId = newClass.Id;
                Db.Update(student);
            }
        }

        foreach (var teacherId in request.TeacherIDs)
        {
            var teacher = await Db.Users.Where(u => u.Id.ToString() == teacherId).FirstOrDefaultAsync();
            if (teacher == null)
            {
                var teacherClass = new TeacherClass();
                teacherClass.ClassId = newClass.Id;
                teacherClass.TeacherId = Guid.Parse(teacherId);
                Db.Add(teacherClass);
            }
        }

        Db.Add(newClass);
        await Db.SaveChangesAsync();
        return Ok(newClass);
    }
    
}