namespace digify.Models;

public class TeacherClass
{
    public Guid TeacherId { get; set; }
    public User Teacher { get; set; }
    
    public Guid ClassId { get; set; }
    public Class Class { get; set; }
}