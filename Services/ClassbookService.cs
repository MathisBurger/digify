using digify.Models;
using digify.Shared;
using Microsoft.EntityFrameworkCore;

namespace digify.Services;

/// <summary>
/// Handles basic calculations and updates on a classbook
/// </summary>
public class ClassbookService
{
    private readonly IContext Db;
    private readonly TimetableService TimetableService;
    private readonly ILogger Logger;
    
    public ClassbookService(IContext db, ILogger logger)
    {
        Db = db;
        TimetableService = new TimetableService(db);
        Logger = logger;
    }

    /// <summary>
    /// Creates a new day entry on the classbook
    /// </summary>
    /// <param name="classbook">The classbook that the day entry should be created on</param>
    /// <param name="dateTime">The date that should be fetched</param>
    /// <returns>The classbook day entry of the fetched day</returns>
    public async Task<ClassbookDayEntry> CreateDayEntry(Classbook classbook, DateTime dateTime)
    {
        var entry = new ClassbookDayEntry();
        entry.Notes = "";
        entry.CurrentDate = dateTime;
        entry.ParentClassbook = classbook;
        Db.ClassbookDayEntries.Attach(entry);
        Db.ClassbookDayEntries.Add(entry);
        await Db.SaveChangesAsync();
        var timetableEvents = await TimetableService.GetElementsForDay(
            classbook.ReferedClass!.Students.First(),
            dateTime.DayOfWeek.ToString()
        );
        foreach (var eventElement in timetableEvents)
        {
            var element = FormatTimetableEventToClassbookDayEntryLesson(eventElement);
            element.Teacher = eventElement.Teacher;
            element.ParentDayEntry = entry;
            Db.ClassbookDayEntryLessons.Attach(element);
            Db.ClassbookDayEntryLessons.Add(element);
            entry.Lessons.Add(element);
        }
        
        if (timetableEvents.Count > 0)
        {
            Db.ClassbookDayEntries.Update(entry);
        }
        await Db.SaveChangesAsync();
        return entry;
    }

    /// <summary>
    /// Formats timetable events to classbook day entry lessons.
    /// </summary>
    /// <param name="element">The element that should be parsed</param>
    /// <returns>The parsed lesson</returns>
    private ClassbookDayEntryLesson FormatTimetableEventToClassbookDayEntryLesson(TimeTableElement element)
    {
        var entry = new ClassbookDayEntryLesson();
        entry.Subject = element.Subject;
        entry.SubjectColor = element.SubjectColor;
        entry.StartTime = element.Start;
        entry.EndTime = element.End;
        entry.Content = "";
        entry.ApprovedByTeacher = false;
        return entry;
    }
    
    /// <summary>
    /// Gets a specific classbook by a student
    /// </summary>
    /// <param name="user">The user that owns the classbook</param>
    /// <returns>The classbook of the student</returns>
    /// <exception cref="Exception">If an error occurs while fetching</exception>
    public async Task<Classbook> GetClassbookByStudent(User user)
    {
        var fetchedUser = (await Db.Users.FindAsync(user.Id));
        if (fetchedUser == null) throw new Exception("Element relation error");
        var fetchedClass = await Db.Classes
            .Where(c => c.Students.Contains(fetchedUser))
            .FirstOrDefaultAsync();
        if (fetchedClass == null) throw new Exception("Element relation error");
        var classbook = await Db.Classbooks
            .Include(c => c.DayEntries)
            .Include(c => c.ReferedClass)
            .Where(c => c.ReferedClass!.Id == fetchedClass.Id)
            .FirstOrDefaultAsync();
        if (classbook == null) throw new Exception("Element relation error");
        if (classbook.Year != DateTime.Now.Year.ToString())
        {
            classbook.Archived = true;
            classbook.ArchivedDate = DateTime.Now;
            classbook.ArchivedName = classbook.ReferedClass!.Name + " - " + classbook.Year;
            var refered = classbook.ReferedClass;
            classbook.ReferedClass = null;
            Db.Classbooks.Update(classbook);
            classbook = new Classbook();
            classbook.Year = DateTime.Now.Year.ToString();
            classbook.Archived = false;
            classbook.ReferedClass = refered;
            Db.Classbooks.Add(classbook);
            await Db.SaveChangesAsync();
        }
        var todayEntry = await Db.ClassbookDayEntries
            .Where(e => e.CurrentDate.DayOfYear == DateTime.Now.DayOfYear)
            .FirstOrDefaultAsync();
        if (null == todayEntry)
        {
            await CreateDayEntry(classbook, DateTime.Now.ToUniversalTime());
            await Db.SaveChangesAsync();
        }
        classbook = await Db.Classbooks
            .Include(c => c.DayEntries)
            .Include(c => c.ReferedClass)
            .Where(c => c.ReferedClass!.Id == fetchedClass.Id)
            .FirstOrDefaultAsync();
        return classbook!;
    }

    /// <summary>
    /// Adds a missing person to the current day entry
    /// </summary>
    /// <param name="classbookID">The ID of the classbook</param>
    /// <param name="missingID">The ID of the missing student</param>
    /// <returns>The updated day entry</returns>
    /// <exception cref="Exception">If an error occurs while updating</exception>
    public async Task<ClassbookDayEntry> AddMissingPerson(Guid classbookID, Guid missingID)
    {
        var classbook = await Db.Classbooks
            .Include(c => c.DayEntries)
            .Include(c => c.ReferedClass)
            .FirstOrDefaultAsync(c => c.Id == classbookID);
        if (classbook == null) throw new Exception("Classbook does not exist");
        var missingStudent = await Db.Users.FirstOrDefaultAsync(u => u.Id == missingID);
        if (missingStudent == null || !missingStudent.Roles.Contains(UserRoles.STUDENT))
        {
            throw new Exception("The requested user is not an student or does not exist");
        }

        var todayEntry = await Db.ClassbookDayEntries
            .FirstOrDefaultAsync(e => e.CurrentDate.DayOfYear == DateTime.Today.DayOfYear 
                                 && e.ParentClassbook.Id == classbookID);
        if (todayEntry == null) throw new Exception("There is no entry existing for the current day");
        if (todayEntry.Missing.FirstOrDefault(u => u.Id == missingID) != null)
        {
            throw new Exception("User is already entered as missing for the current day");
        }
        todayEntry.Missing.Add(missingStudent);
        Db.ClassbookDayEntries.Update(todayEntry);
        await Db.SaveChangesAsync();
        return todayEntry;
    }
    
    /// <summary>
    /// Removes a missing person from the classbook 
    /// </summary>
    /// <param name="classbookID">The ID of the classbook</param>
    /// <param name="missingID">The ID of the missing student</param>
    /// <returns>The updated day entry</returns>
    /// <exception cref="Exception">If an error occurs while updating</exception>
    public async Task<ClassbookDayEntry> RemoveMissingPerson(Guid classbookID, Guid missingID)
    {
        var classbook = await Db.Classbooks
            .Include(c => c.DayEntries)
            .Include(c => c.ReferedClass)
            .FirstOrDefaultAsync(c => c.Id == classbookID);
        if (classbook == null) throw new Exception("Classbook does not exist");
        var missingStudent = await Db.Users.FirstOrDefaultAsync(u => u.Id == missingID);
        if (missingStudent == null || !missingStudent.Roles.Contains(UserRoles.STUDENT))
        {
            throw new Exception("The requested user is not an student or does not exist");
        }

        var todayEntry = await Db.ClassbookDayEntries
            .Include(e => e.Missing)
            .FirstOrDefaultAsync(e => e.CurrentDate.DayOfYear == DateTime.Today.DayOfYear 
                                      && e.ParentClassbook.Id == classbookID);
        if (todayEntry == null) throw new Exception("There is no entry existing for the current day entry");
        if (todayEntry.Missing.FirstOrDefault(u => u.Id == missingID) == null)
        {
            throw new Exception("User is not missing today");
        }
        todayEntry.Missing.Remove(missingStudent);
        Db.ClassbookDayEntries.Update(todayEntry);
        await Db.SaveChangesAsync();
        return todayEntry;
    }
}