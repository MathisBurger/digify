using digify.Models;
using digify.Models.Requests;
using digify.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace digify.Services;

public class TimetableService
{
    private readonly IContext Db;

    public TimetableService(IContext db)
    {
        Db = db;
    }

    private async Task CreateElement(Timetable timetable, RequestTableElement element)
    {
        var teacher = await Db.Users.FindAsync(element.Teacher);
        if (teacher == null || !teacher.Roles.Contains(UserRoles.TEACHER)) throw new Exception("User is not a teacher");
        var newElement = new TimeTableElement();
        newElement.Day = element.Day;
        newElement.End = element.End;
        newElement.Start = element.Start;
        newElement.Room = element.Room;
        newElement.SubjectColor = element.SubjectColor;
        newElement.Teacher = teacher;
        newElement.Subject = element.Subject;
        newElement.Parent = timetable;
        timetable.TableElements.Add(newElement);
        Db.TimeTableElements.Add(newElement);
    }

    private async Task UpdateElement(TimeTableElement newElement, RequestTableElement element)
    {
        var teacher = await Db.Users.FindAsync(element.Teacher);
        if (teacher == null || !teacher.Roles.Contains(UserRoles.TEACHER)) throw new Exception("User is not a teacher");
        newElement.Day = element.Day;
        newElement.End = element.End;
        newElement.Start = element.Start;
        newElement.Room = element.Room;
        newElement.SubjectColor = element.SubjectColor;
        newElement.Teacher = teacher;
        newElement.Subject = element.Subject;
        Db.TimeTableElements.Update(newElement);
    }

    public async Task<Timetable> CreateTimeTable(User user, List<RequestTableElement> elements)
    {
        var timetable = (await Db.Timetables.FindAsync(user.Timetable!.Id))!;
        foreach (var element in elements)
        {
            CreateElement(timetable, element);
        }

        Db.Timetables.Update(timetable);
        await Db.SaveChangesAsync();
        return timetable;
    }

    public async Task<Timetable> DeleteTimeTable(Guid tableId)
    {
        var table = (await Db.Timetables.FindAsync(tableId))!;
        foreach (var element in table.TableElements)
        {
            Db.TimeTableElements.Remove(element);
        }
        table.TableElements.Clear();
        Db.Timetables.Update(table);
        await Db.SaveChangesAsync();
        return table;
    }

    public async Task<Timetable> UpdateTimetableForUser(User user, List<RequestTableElement> elements)
    {
        var timetable = await Db.Timetables.Where(t => t.OwningUser.Id == user.Id)
            .FirstOrDefaultAsync();
        foreach (var element in elements)
        {
            var existingElement = await Db.TimeTableElements
                .Where(e => e.Parent.Id == timetable!.Id && e.Id == element.Id)
                .FirstOrDefaultAsync();
            if (existingElement == null)
            {
                await CreateElement(timetable!, element);
            }
            else
            {
                await UpdateElement(existingElement, element);
            }
        }

        await Db.SaveChangesAsync();
        return timetable!;
    }

    public async Task<Timetable> CreateTimetableForClass(Class fetchedClass, List<RequestTableElement> elements)
    {
        foreach (var user in fetchedClass.Students)
        {
            await CreateTimeTable(user, elements);
        }

        return (await Db.Timetables.FindAsync(fetchedClass.Students.First().Timetable!.Id))!;
    }
    
    public async Task<Timetable> DeleteTimetableForClass(Class fetchedClass)
    {
        foreach (var user in fetchedClass.Students)
        {
            await DeleteTimeTable(user.Timetable!.Id);
        }

        return (await Db.Timetables.FindAsync(fetchedClass.Students.First().Timetable!.Id))!;
    }
    
    public async Task UpdateTimetableForClass(Class fetchedClass, List<RequestTableElement> elements)
    {
        var students = await Db.Users
            .Where(u => u.SchoolClass!.Id == fetchedClass.Id)
            .ToListAsync();
        foreach (var user in students)
        {
            await UpdateTimetableForUser(user, elements);
        }
    }

    public async Task<List<TimeTableElement>> GetElementsForDay(User user, string day)
    {
        var fetchedTimetable = await Db.Timetables
            .Where(t => t.OwningUser.Id == user.Id)
            .Include(t => t.TableElements)
            .FirstOrDefaultAsync();
        if (fetchedTimetable == null) throw new Exception("User has no timetable");
        var entries = new List<TimeTableElement>();
        foreach (var element in fetchedTimetable.TableElements)
        {
            entries.Add((await Db.TimeTableElements
                    .Include(e => e.Teacher)
                    .Where(e => e.Id == element.Id)
                    .FirstOrDefaultAsync())!
            );
        }

        fetchedTimetable.TableElements = entries;
        return fetchedTimetable.TableElements.Where(e => e.Day == ParseDayOfWeekToNumber(day)).ToList();
    }

    private string ParseDayOfWeekToNumber(string dayOfWeek)
    {
        switch (dayOfWeek)
        {
            case "Monday": return "1";
            case "Tuesday": return "2";
            case "Wednesday": return "3";
            case "Thursday": return "4";
            case "Friday": return "5";
            default: return "6";
        }
    }
}