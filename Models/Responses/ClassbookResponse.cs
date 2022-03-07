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
            .Where(c => c.Id == classbook.Id).FirstOrDefaultAsync();
        return fetchedClassbook;
    }
}