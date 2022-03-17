using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace digify.Models.Requests;

public class RequestClassbookLesson
{
    [Required]
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    [JsonPropertyName("content")]
    public string Content { get; set; }
    
    [Required]
    [JsonPropertyName("approved_by_teacher")]
    public bool ApprovedByTeacher { get; set; }
}