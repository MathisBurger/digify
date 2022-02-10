using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace digify.Models;

public class TimeTableElement : Entity
{
    [JsonPropertyName("start_time")]
    public DateTime Start { get; set; }
    
    public string Day { get; set; }
    
    [JsonPropertyName("end_time")]
    public DateTime End { get; set; }
    
    public string Teacher { get; set; }
    
    public string Room { get; set; }
    
    public string SubjectColor { get; set; }
    
    public Timetable Parent { get; set; }
}