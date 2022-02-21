using System.Text.Json.Serialization;

namespace digify.Models.Requests;

public class RequestTableElement
{
    [JsonPropertyName("start_time")]
    public DateTime Start { get; set; }
    
    [JsonPropertyName("day")]
    public string Day { get; set; }
    
    [JsonPropertyName("end_time")]
    public DateTime End { get; set; }
    
    [JsonPropertyName("teacher")]
    public string Teacher { get; set; }
    
    [JsonPropertyName("room")]
    public string Room { get; set; }
    
    [JsonPropertyName("subject_color")]
    public string SubjectColor { get; set; }
    
    [JsonPropertyName("subject")]
    public string Subject { get; set; }
}