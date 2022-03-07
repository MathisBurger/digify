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
            return Ok(await new ClassResponse(Db).ParseList(
                new List<Class>() {Db.Classes.ToArray()[0]}));
        }

        return Ok(
            await new ClassResponse(Db).ParseList(
                Db.Classes.Where(c => c.Students.Contains(AuthorizedUser)).ToList()
                )
            );
    }

    /// <summary>
    /// Fetches a specific class from the database
    /// </summary>
    /// <param name="id">The id of the class</param>
    [HttpGet("/class/{id}")]
    [TypeFilter(typeof(RequiresAuthorization))]
    public async Task<ActionResult<Class>> GetClass([FromRoute] Guid id)
    {
        var c = await Db.Classes.FindAsync(id);
        if (null == c)
        {
            return BadRequest();
        }

        return Ok(await new ClassResponse(Db).ParseSingle(c));
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
        var classbook = new Classbook();
        classbook.Archived = false;
        classbook.Year = DateTime.Now.Year.ToString();
        classbook.ReferedClass = newClass;
        newClass.Name = request.Name;
        newClass.Classbook = classbook;
        Db.Classes.Attach(newClass);
        Db.Classes.Add(newClass);
        Db.Classbooks.Attach(classbook);
        Db.Classbooks.Add(classbook);
        foreach (var studentId in request.StudentsIDs)
        {
            var student = await Db.Users.FindAsync(Guid.Parse(studentId));
            if (student != null)
            {
                student.SchoolClass = newClass;
                newClass.Students.Add(student);
                Db.Update(student);
            }
        }
        
        foreach (var teacherId in request.TeacherIDs)
        {
            var teacher = await Db.Users.FindAsync(Guid.Parse(teacherId));
            if (teacher == null) continue;
            teacher.Classes.Add(newClass);
            newClass.Teachers.Add(teacher);
            Db.Update(teacher);
        }
        
        await Db.SaveChangesAsync();
        Logger.LogInformation("Created class");
        return Ok(newClass);
    }

    /// <summary>
    /// Deletes a class from the system
    /// </summary>
    /// <param name="id">The ID of the class that should be deleted</param>
    [HttpDelete("/class/deleteClass/{id}")]
    [TypeFilter(typeof(RequiresAuthorization))]
    public async Task<ActionResult> DeleteClass([FromRoute] Guid id)
    {
        if (!Authorization.IsGranted(AuthorizedUser, ClassVoter.CAN_DELETE, new ClassVoter(null, Db)))
        {
            return Unauthorized();
        }

        var deleteableClass = await Db.Classes.FindAsync(id);
        if (deleteableClass == null)
        {
            return BadRequest();
        }
        
        Db.Remove(deleteableClass);
        await Db.SaveChangesAsync();
        return Ok();
    }
    
}