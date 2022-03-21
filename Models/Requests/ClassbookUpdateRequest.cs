using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace digify.Models.Requests;

public class ClassbookUpdateRequest
{
    [Required]
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    [JsonPropertyName("lesson_to_update")]
    public RequestClassbookLesson? LessonToUpdate { get; set; }
    
    [JsonPropertyName("notes")]
    public string? Notes { get; set; }
}