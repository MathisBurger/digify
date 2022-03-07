using digify.Models;
using digify.Shared;
using Microsoft.EntityFrameworkCore;

namespace digify.Services;

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
        var todayEntry = await Db.ClassbookDayEntries
            .Where(e => e.CurrentDate.DayOfYear == DateTime.Now.DayOfYear)
            .FirstOrDefaultAsync();
        if (null == todayEntry)
        {
            await CreateDayEntry(classbook, DateTime.Now.ToUniversalTime());
        }
        return classbook;
    }
}