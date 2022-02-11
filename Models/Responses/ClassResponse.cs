﻿using digify.Controllers;
using digify.Shared;
using Microsoft.EntityFrameworkCore;

namespace digify.Models.Responses;

public class ClassResponse
{
    private readonly IContext Db;
    
    public ClassResponse(IContext db)
    {
        Db = db;
    }

    public async Task<List<Class>> ParseList(List<Class> classes)
    {
        var newList = new List<Class>();
        foreach (var cClass in classes)
        {
            var fetchedClass = await Db.Classes.FindAsync(cClass.Id);
            fetchedClass!.Students = await Db.Users.Where(
                u => u.SchoolClass != null && u.SchoolClass.Id == cClass.Id
                ).ToListAsync();
            fetchedClass!.Teachers = await Db.Users.Where(t => t.Classes.Contains(cClass)).ToListAsync();
            newList.Add(fetchedClass!);
        }

        return newList;
    }
}