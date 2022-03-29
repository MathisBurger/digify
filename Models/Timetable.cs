using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace digify.Models;

/// <summary>
/// The timetable entity
/// </summary>
public class Timetable : Entity
{
    /// <summary>
    /// All elements of the timetable
    /// </summary>
    [JsonPropertyName("table_elements")]
    [InverseProperty("Parent")]
    public IList<TimeTableElement> TableElements { get; set; }
    
    /// <summary>
    /// The user that owns the timetable
    /// </summary>
    [InverseProperty("Timetable")]
    [ForeignKey("Id")]
    public User OwningUser { get; set; }

    public Timetable()
    {
        TableElements = new List<TimeTableElement>();
    }
}