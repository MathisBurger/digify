using digify.Models;
using digify.Shared;
using Microsoft.EntityFrameworkCore;

namespace digify.Services;

public class ClassbookService
{
    private readonly IContext Db;
    
    public ClassbookService(IContext db)
    {
        Db = db;
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
            .Where(c => c.ReferedClass!.Id == fetchedClass.Id)
            .FirstOrDefaultAsync();
        if (classbook == null) throw new Exception("Element relation error");
        classbook.DayEntries = await Db.ClassbookDayEntries
            .Where(e => e.ParentClassbook.Id == classbook.Id)
            .ToListAsync();
        return classbook;
    }
}