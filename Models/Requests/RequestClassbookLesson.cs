using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace digify.Models.Requests;

/// <summary>
/// A scaffold classbook lesson used in requests
/// </summary>
public class RequestClassbookLesson
{
    /// <summary>
    /// The ID of the lesson
    /// </summary>
    [Required]
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    /// <summary>
    /// The content of the lesson that should be updated
    /// </summary>
    [JsonPropertyName("content")]
    public string Content { get; set; }
    
    /// <summary>
    /// If the lesson was approved by the teacher of the lesson
    /// </summary>
    [Required]
    [JsonPropertyName("ApprovedByTeacher")]
    public bool ApprovedByTeacher { get; set; }
}