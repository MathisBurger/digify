using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace digify.Models.Requests;

/// <summary>
/// Request type used for updating a classbook lesson
/// </summary>
public class ClassbookUpdateRequest
{
    /// <summary>
    /// The ID of the classbook
    /// </summary>
    [Required]
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    /// <summary>
    /// The lesson that should be updated
    /// </summary>
    [JsonPropertyName("lesson_to_update")]
    public RequestClassbookLesson? LessonToUpdate { get; set; }
    
    /// <summary>
    /// The notes that should be updated
    /// </summary>
    [JsonPropertyName("notes")]
    public string? Notes { get; set; }
}