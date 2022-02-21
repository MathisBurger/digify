using digify.Models;
using digify.Models.Requests;
using digify.Shared;

namespace digify.Services;

public class TimetableService
{
    private readonly IContext Db;

    public TimetableService(IContext db)
    {
        Db = db;
    }

    public async Task<Timetable> CreateTimeTable(User user, List<RequestTableElement> elements)
    {
        var timetable = (await Db.Timetables.FindAsync(user.Timetable!.Id))!;
        foreach (var element in elements)
        {
            var newElement = new TimeTableElement();
            newElement.Day = element.Day;
            newElement.End = element.End;
            newElement.Start = element.Start;
            newElement.Room = element.Room;
            newElement.SubjectColor = element.SubjectColor;
            newElement.Teacher = element.Teacher;
            newElement.Parent = timetable;
            timetable.TableElements.Add(newElement);
            Db.TimeTableElements.Add(newElement);
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

}