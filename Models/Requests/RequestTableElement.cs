using System.Text.Json.Serialization;

namespace digify.Models.Requests;

/// <summary>
/// A table element used in requests for updating or creating new ones
/// </summary>
public class RequestTableElement
{
    /// <summary>
    /// The start time of the lesson
    /// </summary>
    [JsonPropertyName("start_time")]
    public DateTime Start { get; set; }
    
    /// <summary>
    /// The day of week of the entry
    /// </summary>
    [JsonPropertyName("day")]
    public string Day { get; set; }
    
    /// <summary>
    /// The end time of the lesson
    /// </summary>
    [JsonPropertyName("end_time")]
    public DateTime End { get; set; }
    
    /// <summary>
    /// The ID of the teacher that is assigned to this lesson
    /// </summary>
    [JsonPropertyName("teacher")]
    public Guid Teacher { get; set; }
    
    /// <summary>
    /// The room the user/class is teached in
    /// </summary>
    [JsonPropertyName("room")]
    public string Room { get; set; }
    
    /// <summary>
    /// The color of the subject that is displayed in the frontend
    /// </summary>
    [JsonPropertyName("subject_color")]
    public string SubjectColor { get; set; }
    
    /// <summary>
    /// The name of the subject
    /// </summary>
    [JsonPropertyName("subject")]
    public string Subject { get; set; }
    
    /// <summary>
    /// The ID of the element, if it already exists
    /// </summary>
    [JsonPropertyName("id")]
    public Guid? Id { get; set; }
}