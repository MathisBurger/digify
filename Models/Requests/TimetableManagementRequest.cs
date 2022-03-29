using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace digify.Models.Requests;

/// <summary>
/// The general purpose request used for performing a timetable update
/// </summary>
public class TimetableManagementRequest
{
    /// <summary>
    /// All timetable elements that should be included and affected by the request
    /// </summary>
    [JsonPropertyName("request_table_elements")]
    public List<RequestTableElement>? RequestTableElements { get; set; }
    
    /// <summary>
    /// The ID of the user that owns the timetable
    /// </summary>
    [JsonPropertyName("user_id")]
    public Guid? UserId { get; set; }
    
    /// <summary>
    /// The ID of the class that owns the timetable
    /// </summary>
    [JsonPropertyName("class_id")]
    public Guid? ClassId { get; set; }
}