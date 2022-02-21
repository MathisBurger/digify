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
        var fetched = await Db.Timetables.FindAsync(unparsed.Id);
        fetched!.TableElements = await Db.TimeTableElements
            .Where(e => e.Parent.Id == fetched.Id)
            .ToListAsync();
        return fetched;
    }
}