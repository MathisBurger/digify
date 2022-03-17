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
    
    [HttpPost("/classbook/updateLesson")]
    [TypeFilter(typeof(RequiresAuthorization))]
    public async Task<ActionResult<Classbook>> UpdateClassbookLessonForClass([FromBody] ClassbookUpdateRequest request)
    {
        if (request.LessonToUpdate == null)
        {
            return BadRequest("Invalid request body");
        }

        var rawClassbook = await Db.Classbooks.FindAsync(request.Id);
        var classbook = await new ClassbookResponse(Db).ParseSingle(rawClassbook);
        if (classbook == null) throw new Exception("DB error");
        var lesson = await Db.ClassbookDayEntryLessons
            .Include(l => l.ParentDayEntry)
            .Where(l => l.Id == request.LessonToUpdate.Id)
            .FirstOrDefaultAsync();
        if (lesson == null) throw new Exception("Lesson does not exist");
        if (classbook.DayEntries.FirstOrDefault(e => e.Id == lesson.ParentDayEntry.Id) == null)
        {
            throw new Exception("Lesson does not exist on classbook");
        }

        lesson = UpdateLessonOfRequestWithoutSaving(lesson, request.LessonToUpdate);
        Db.ClassbookDayEntryLessons.Update(lesson);
        await Db.SaveChangesAsync();
        return (await new ClassbookResponse(Db).ParseSingle(classbook))!;
    }

    private ClassbookDayEntryLesson UpdateLessonOfRequestWithoutSaving(ClassbookDayEntryLesson lesson,
        RequestClassbookLesson request)
    {
        lesson.Content = request.Content;
        lesson.ApprovedByTeacher = request.ApprovedByTeacher;
        return lesson;
    }
    
}