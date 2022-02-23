using System.ComponentModel.DataAnnotations.Schema;

namespace digify.Models;

public class ClassbookDayEntry : Entity
{
    [InverseProperty("DayEntries")]
    public Classbook ParentClassbook { get; set; }
    
    public DateTime CurrentDate { get; set; }
    
    [InverseProperty("ParentDayEntry")]
    public ICollection<ClassbookDayEntryLesson> Lessons { get; set; }
    
    [InverseProperty("MissedDays")]
    public ICollection<User> Missing { get; set; }
    
    public string Notes { get; set; }

}