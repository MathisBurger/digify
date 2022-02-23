using System.ComponentModel.DataAnnotations.Schema;

namespace digify.Models;

public class Classbook : Entity
{
    [ForeignKey("Id")]
    [InverseProperty("Classbook")]
    public Class? ReferedClass { get; set; }
    
    public string Year { get; set; }
    
    public bool Archived { get; set; }
    
    public string? ArchivedName { get; set; }
    
    public DateTime? ArchivedDate { get; set; }
    
    [InverseProperty("ParentClassbook")]
    public ICollection<ClassbookDayEntry> DayEntries { get; set; }

    public Classbook()
    {
        DayEntries = new List<ClassbookDayEntry>();
    }
}