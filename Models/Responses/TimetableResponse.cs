using digify.Shared;
using Microsoft.EntityFrameworkCore;

namespace digify.Models.Responses;

public class TimetableResponse
{
    private readonly IContext Db;
    
    public TimetableResponse(IContext db)
    {
        Db = db;
    }

    /// <summary>
    /// Fetches all subfields of the relation
    /// </summary>
    /// <param name="unparsed">The timetable with unfetched subfields</param>
    /// <returns>Entity with fetched subfields</returns>
    public async Task<Timetable> FetchTimetable(Timetable unparsed)
    {
        var fetched = await Db.Timetables.Include(t => t.TableElements)
            .Where(t => t.Id == unparsed.Id)
            .FirstOrDefaultAsync();
        var elements = await Db.TimeTableElements
            .Include(e => e.Teacher)
            .Where(e => e.Parent.Id == unparsed.Id)
            .ToListAsync();
        if (fetched == null) throw new Exception("Timetable not found");
        fetched.TableElements = elements;
        return fetched;
    }
}