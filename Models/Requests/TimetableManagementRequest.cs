using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace digify.Models.Requests;

public class TimetableManagementRequest
{
    [JsonPropertyName("request_table_elements")]
    public List<RequestTableElement>? RequestTableElements { get; set; }
    
    [JsonPropertyName("user_id")]
    public Guid? UserId { get; set; }
    
    [JsonPropertyName("class_id")]
    public Guid? ClassId { get; set; }
}