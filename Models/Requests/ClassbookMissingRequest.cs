using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace digify.Models.Requests;

/// <summary>
/// Request type used for adding and removing missing users
/// </summary>
public class ClassbookMissingRequest
{
    /// <summary>
    /// The ID of the classbook that should be updated
    /// </summary>
    [Required]
    [JsonPropertyName("classbook_id")]
    public Guid ClassbookId { get; set; }
    
    /// <summary>
    /// The ID of the missing student 
    /// </summary>
    [Required]
    [JsonPropertyName("missing_id")]
    public Guid MissingId { get; set; }
}