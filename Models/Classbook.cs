using System.ComponentModel.DataAnnotations.Schema;

namespace digify.Models;

/// <summary>
/// The classbook entity
/// </summary>
public class Classbook : Entity
{
    /// <summary>
    /// The parent class
    /// </summary>
    [ForeignKey("Id")]
    [InverseProperty("Classbook")]
    public Class? ReferedClass { get; set; }
    
    /// <summary>
    /// The year of the classbook
    /// </summary>
    public string Year { get; set; }
    
    /// <summary>
    /// If the classbook is archived
    /// </summary>
    public bool Archived { get; set; }
    
    /// <summary>
    /// The name of the archived classbook
    /// </summary>
    public string? ArchivedName { get; set; }
    
    /// <summary>
    /// The date of the archive action
    /// </summary>
    public DateTime? ArchivedDate { get; set; }
    
    /// <summary>
    /// All day entries of the classbook
    /// </summary>
    [InverseProperty("ParentClassbook")]
    public ICollection<ClassbookDayEntry> DayEntries { get; set; }

    public Classbook()
    {
        DayEntries = new List<ClassbookDayEntry>();
    }
}