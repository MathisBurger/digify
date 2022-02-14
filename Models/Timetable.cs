using System.ComponentModel.DataAnnotations.Schema;

namespace digify.Models;

public class Timetable : Entity
{
    [InverseProperty("Parent")]
    public IList<TimeTableElement> TableElements { get; set; }
    
    [InverseProperty("Timetable")]
    public User OwningUser { get; set; }

    public Timetable()
    {
        TableElements = new List<TimeTableElement>();
    }
}