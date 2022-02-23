using System.ComponentModel.DataAnnotations.Schema;

namespace digify.Models;

public class ClassbookDayEntryLesson : Entity
{
    
    [InverseProperty("Lessons")]
    public ClassbookDayEntry ParentDayEntry { get; set; }
    
    public string Subject { get; set; }
    
    public string Content { get; set; }
    
    public DateTime StartTime { get; set; }
    
    public DateTime EndTime { get; set; }
    
    public string SubjectColor { get; set; }
    
    public bool ApprovedByTeacher { get; set; }
}