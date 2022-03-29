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

    /// <summary>
    /// Fetches the classbook of the current logged-in student
    /// </summary>
    [HttpGet("/classbook")]
    [TypeFilter(typeof(RequiresAuthorization))]
    public async Task<ActionResult<Classbook>> GetClassbookOfCurrentUser()
    {
        if (!Authorization.IsGranted(AuthorizedUser, ClassbookVoter.GET_OWN_CLASSBOOK, new ClassbookVoter(Db)))
        {
            return BadRequest("You do not own any classbook, because you are not a member of a class");
        }

        return Ok(await new ClassbookResponse(Db).ParseSingle(await ClassbookService.GetClassbookByStudent(AuthorizedUser)));
    }
    
    /// <summary>
    /// Updates a specific lesson of the current day entry
    /// </summary>
    [HttpPost("/classbook/updateLesson")]
    [TypeFilter(typeof(RequiresAuthorization))]
    public async Task<ActionResult<Classbook>> UpdateClassbookLessonForClass([FromBody] ClassbookUpdateRequest request)
    {
        if (!Authorization.IsGranted(AuthorizedUser, ClassbookVoter.UPDATE_LESSON, new ClassbookVoter(Db)))
        {
            return Unauthorized();
        }
        if (request.LessonToUpdate == null 
            || (request.LessonToUpdate.Content == String.Empty && request.LessonToUpdate.Content != ""))
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

    /// <summary>
    /// Adds a student to the missing list of the current day entry
    /// </summary>
    [HttpPost("/classbook/addMissing")]
    [TypeFilter(typeof(RequiresAuthorization))]
    public async Task<ActionResult<Classbook>> AddMissingPerson([FromBody] ClassbookMissingRequest request)
    {
        if (!Authorization.IsGranted(AuthorizedUser, ClassbookVoter.ADD_MISSING_PERSON, new ClassbookVoter(Db)))
        {
            return Unauthorized();
        }
        await ClassbookService.AddMissingPerson(request.ClassbookId, request.MissingId);
        return (await new ClassbookResponse(Db).ParseSingle(await Db.Classbooks.FindAsync(request.ClassbookId)))!;
    }

    /// <summary>
    /// Removes a student from the missing list of the current day entry
    /// </summary>
    [HttpDelete("/classbook/removeMissing")]
    [TypeFilter(typeof(RequiresAuthorization))]
    public async Task<ActionResult<Classbook>> RemoveMissingPerson([FromBody] ClassbookMissingRequest request)
    {
        if (!Authorization.IsGranted(AuthorizedUser, ClassbookVoter.REMOVE_MISSING_PERSON, new ClassbookVoter(Db)))
        {
            return Unauthorized();
        }
        await ClassbookService.RemoveMissingPerson(request.ClassbookId, request.MissingId);
        return (await new ClassbookResponse(Db).ParseSingle(await Db.Classbooks.FindAsync(request.ClassbookId)))!;
    }

    /// <summary>
    /// Updates a lesson of the request without flushing the data into the database
    /// </summary>
    private ClassbookDayEntryLesson UpdateLessonOfRequestWithoutSaving(ClassbookDayEntryLesson lesson,
        RequestClassbookLesson request)
    {
        lesson.Content = request.Content;
        lesson.ApprovedByTeacher = request.ApprovedByTeacher;
        return lesson;
    }


    /// <summary>
    /// Updates the notes of the current day entry
    /// </summary>
    [HttpPost("/classbook/updateNotes")]
    [TypeFilter(typeof(RequiresAuthorization))]
    public async Task<ActionResult<Classbook>> UpdateNotes([FromBody] ClassbookUpdateRequest request)
    {
        if (!Authorization.IsGranted(AuthorizedUser, ClassbookVoter.UPDATE_NOTES, new ClassbookVoter(Db)))
        {
            return Unauthorized();
        }
        if (request.Notes == null)
        {
            return BadRequest();
        }

        var dayEntry = await Db.ClassbookDayEntries
            .Where(e => e.ParentClassbook.Id == request.Id && e.CurrentDate.DayOfYear == DateTime.Now.DayOfYear)
            .FirstOrDefaultAsync();
        if (dayEntry == null) return BadRequest();
        dayEntry.Notes = request.Notes;
        Db.ClassbookDayEntries.Update(dayEntry);
        await Db.SaveChangesAsync();
        return (await new ClassbookResponse(Db).ParseSingle(await Db.Classbooks.FindAsync(request.Id)))!;
    }
}