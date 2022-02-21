using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace digify.Models;

public class Timetable : Entity
{
    [JsonPropertyName("table_elements")]
    [InverseProperty("Parent")]
    public IList<TimeTableElement> TableElements { get; set; }
    
    [InverseProperty("Timetable")]
    [ForeignKey("Id")]
    public User OwningUser { get; set; }

    public Timetable()
    {
        TableElements = new List<TimeTableElement>();
    }
}