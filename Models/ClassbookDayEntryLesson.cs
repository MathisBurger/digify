using System.ComponentModel.DataAnnotations.Schema;

namespace digify.Models;

/// <summary>
/// A lesson of a classbook day entry
/// </summary>
public class ClassbookDayEntryLesson : Entity
{
    /// <summary>
    /// The parent day entry of the lesson
    /// </summary>
    [InverseProperty("Lessons")]
    public ClassbookDayEntry ParentDayEntry { get; set; }
    
    /// <summary>
    /// The subject of the lesson
    /// </summary>
    public string Subject { get; set; }
    
    /// <summary>
    /// The content that has been added by the teacher
    /// </summary>
    public string Content { get; set; }
    
    /// <summary>
    /// The start time of the lesson
    /// </summary>
    public DateTime StartTime { get; set; }
    
    /// <summary>
    /// The end time of the lesson
    /// </summary>
    public DateTime EndTime { get; set; }
    
    /// <summary>
    /// The color of the subject
    /// </summary>
    public string SubjectColor { get; set; }
    
    /// <summary>
    /// If the lesson if approved by the teacher
    /// </summary>
    public bool ApprovedByTeacher { get; set; }
    
    /// <summary>
    /// The teacher that is assigned to the lesson
    /// </summary>
    public User? Teacher { get; set; }
}