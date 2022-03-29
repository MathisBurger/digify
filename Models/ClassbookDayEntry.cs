using System.ComponentModel.DataAnnotations.Schema;

namespace digify.Models;

/// <summary>
/// A day entry of a classbook
/// </summary>
public class ClassbookDayEntry : Entity
{
    /// <summary>
    /// The parent classbook
    /// </summary>
    [InverseProperty("DayEntries")]
    public Classbook ParentClassbook { get; set; }
    
    /// <summary>
    /// The current date of the entry
    /// </summary>
    public DateTime CurrentDate { get; set; }
    
    /// <summary>
    /// All lessons of the day entry
    /// </summary>
    [InverseProperty("ParentDayEntry")]
    public ICollection<ClassbookDayEntryLesson> Lessons { get; set; }
    
    /// <summary>
    /// All missing students
    /// </summary>
    [InverseProperty("MissedDays")]
    public ICollection<User> Missing { get; set; }
    
    /// <summary>
    /// All notes of the day entry
    /// </summary>
    public string Notes { get; set; }

    public ClassbookDayEntry()
    {
        Lessons = new List<ClassbookDayEntryLesson>();
        Missing = new List<User>();
    }

}