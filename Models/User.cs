using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace digify.Models;

/// <summary>
/// The user entity that can log in into the webpage
/// </summary>
public class User: Entity
{
    /// <summary>
    /// The username of the user
    /// </summary>
    public string Username { get; set; }
    
    /// <summary>
    /// The hashed password of the user
    /// </summary>
    [System.Text.Json.Serialization.JsonIgnore]
    public string Password { get; set; }
    
    /// <summary>
    /// All roles of the user
    /// </summary>
    public string[] Roles { get; set; }
    
    /// <summary>
    /// The class the user is assigned to.
    /// NOTE: Is null if the user is teacher or admin
    /// </summary>
    [InverseProperty("Students")]
    public Class? SchoolClass { get; set; }
    
    /// <summary>
    /// All classes of a user.
    /// NOTE: Only teachers are having assigned classes.
    /// </summary>
    [InverseProperty("Teachers")]
    public ICollection<Class> Classes { get; set; }
    
    /// <summary>
    /// The timetable that is assigned to the user
    /// </summary>
    [InverseProperty("OwningUser")]
    public Timetable? Timetable { get; set; }
    
    /// <summary>
    /// All days that the student was not in school
    /// </summary>
    [InverseProperty("Missing")]
    public ICollection<ClassbookDayEntry> MissedDays { get; set; }


    public User()
    {
        SchoolClass = null;
        Timetable = null;
        Classes = new List<Class>();
        MissedDays = new List<ClassbookDayEntry>();
    }
}