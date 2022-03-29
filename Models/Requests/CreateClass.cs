using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace digify.Models.Requests;

/// <summary>
/// Request type used for class creation
/// </summary>
public class CreateClass
{
    /// <summary>
    /// The name of the class
    /// </summary>
    [JsonPropertyName("name")]
    [Required]
    public string Name { get; set; }
    
    /// <summary>
    /// All IDs of all teachers assigned to this class
    /// </summary>
    [JsonPropertyName("teacher_ids")]
    [Required]
    public string[] TeacherIDs { get; set; }
    
    /// <summary>
    /// ALl IDs of the students assigned to this class
    /// </summary>
    [JsonPropertyName("student_ids")]
    [Required]
    public string[] StudentsIDs { get; set; }
}