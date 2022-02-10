using System.ComponentModel.DataAnnotations.Schema;

namespace digify.Models;

public class Timetable : Entity
{
    [ForeignKey("Id")]
    public IList<TimeTableElement> TableElements { get; set; }
    
    [ForeignKey("Id")]
    public User OwningUser { get; set; }
}