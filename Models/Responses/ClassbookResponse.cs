using digify.Shared;
using Microsoft.EntityFrameworkCore;

namespace digify.Models.Responses;

public class ClassbookResponse
{
    
    private readonly IContext Db;
    
    public ClassbookResponse(IContext db)
    {
        Db = db;
    }

    public async Task<Classbook?> ParseSingle(Classbook? classbook)
    {
        if (classbook == null) return null;
        var fetchedClassbook = await Db.Classbooks
            .Include(c => c.ReferedClass)
            .Include(c => c.DayEntries)
            .Where(c => c.Id == classbook.Id)
            .FirstOrDefaultAsync();
        var entries = new List<ClassbookDayEntry>();
        if (fetchedClassbook == null) return null;
        foreach (var classbookDayEntry in fetchedClassbook.DayEntries)
        {
            entries.Add((await Db.ClassbookDayEntries
                    .Include(e => e.Lessons)
                    .Where(e => e.Id == classbookDayEntry.Id)
                    .FirstOrDefaultAsync())!
            );
        }

        fetchedClassbook.DayEntries = entries;
        return fetchedClassbook;
    }
}