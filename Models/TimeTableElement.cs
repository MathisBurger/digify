using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace digify.Models;

/// <summary>
/// An element of the timetable
/// </summary>
public class TimeTableElement : Entity
{
    /// <summary>
    /// The start time of a timetable element
    /// </summary>
    [JsonPropertyName("start_time")]
    public DateTime Start { get; set; }
    
    /// <summary>
    /// The day of week of a timetable element
    /// </summary>
    [JsonPropertyName("day")]
    public string Day { get; set; }
    
    /// <summary>
    /// The end time of a timetable element
    /// </summary>
    [JsonPropertyName("end_time")]
    public DateTime End { get; set; }
    
    /// <summary>
    /// The teacher that is assigned to the element
    /// </summary>
    [JsonPropertyName("teacher")]
    public User Teacher { get; set; }
    
    /// <summary>
    /// The room the user is teached in
    /// </summary>
    [JsonPropertyName("room")]
    public string Room { get; set; }
    
    /// <summary>
    /// The color of the subject that is displayed in the frontend
    /// </summary>
    [JsonPropertyName("subject_color")]
    public string SubjectColor { get; set; }
    
    /// <summary>
    /// The subject of the timetable element
    /// </summary>
    [JsonPropertyName("subject")]
    public string Subject { get; set; }
    
    /// <summary>
    /// The parent timetable
    /// </summary>
    [InverseProperty("TableElements")]
    public Timetable Parent { get; set; }
}