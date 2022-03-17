using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace digify.Models.Requests;

public class ClassbookMissingRequest
{
    [Required]
    [JsonPropertyName("classbook_id")]
    public Guid ClassbookId { get; set; }
    
    [Required]
    [JsonPropertyName("missing_id")]
    public Guid MissingId { get; set; }
}