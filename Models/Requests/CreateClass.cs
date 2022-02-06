using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace digify.Models.Requests;

public class CreateClass
{
    [JsonPropertyName("name")]
    [Required]
    public string Name { get; set; }
    
    [JsonPropertyName("teacher_ids")]
    [Required]
    public string[] TeacherIDs { get; set; }
    
    [JsonPropertyName("student_ids")]
    [Required]
    public string[] StudentsIDs { get; set; }
}