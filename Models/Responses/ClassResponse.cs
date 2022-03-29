using digify.Controllers;
using digify.Shared;
using Microsoft.EntityFrameworkCore;

namespace digify.Models.Responses;

/// <summary>
/// Response pattern that can display a class with all relations
/// </summary>
public class ClassResponse
{
    private readonly IContext Db;
    
    public ClassResponse(IContext db)
    {
        Db = db;
    }

    /// <summary>
    /// Parses a single class and fetches all realtions
    /// </summary>
    /// <param name="cClass">The class with many unfetched parameters</param>
    /// <returns>The class with all relations fetched</returns>
    public async Task<Class?> ParseSingle(Class cClass)
    {
        var fetchedClass = await Db.Classes
            .Include(c => c.Students)
            .Include(c => c.Teachers)
            .Where(c => cClass.Id == c.Id).FirstOrDefaultAsync();
        return fetchedClass;
    }
    

    /// <summary>
    /// Parses a list of classes
    /// </summary>
    /// <param name="classes">All classes that should be fetched</param>
    /// <returns>All fetched classes</returns>
    public async Task<List<Class>> ParseList(List<Class> classes)
    {
        var newList = new List<Class>();
        foreach (var cClass in classes)
        {
            newList.Add(await ParseSingle(cClass)!);
        }
        return newList;
    }
}