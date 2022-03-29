using digify.Shared;
using Microsoft.EntityFrameworkCore;

namespace digify.Models.Responses;

/// <summary>
/// Response pattern that fetches all relations of a classbook
/// </summary>
public class ClassbookResponse
{
    
    private readonly IContext Db;
    
    public ClassbookResponse(IContext db)
    {
        Db = db;
    }

    /// <summary>
    /// Fetches all relations of a single classbook
    /// </summary>
    /// <param name="classbook">The unfetched classbook</param>
    /// <returns>The classbook with all relations fetched</returns>
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
            var entry = (await Db.ClassbookDayEntries
                .Include(e => e.Lessons)
                .Include(e => e.Missing)
                .Where(e => e.Id == classbookDayEntry.Id)
                .FirstOrDefaultAsync())!;
            var lessons = new List<ClassbookDayEntryLesson>();
            foreach (var lesson in entry.Lessons)
            {
                lessons.Add((await Db.ClassbookDayEntryLessons
                    .Include(l => l.Teacher)
                    .Where(l => l.Id == lesson.Id)
                    .FirstOrDefaultAsync())!
                    );
            }

            entry.Lessons = lessons;
            entries.Add(entry);
        }

        fetchedClassbook.DayEntries = entries;
        return fetchedClassbook;
    }
}