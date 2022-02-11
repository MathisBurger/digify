using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace digify.Models;

public class Class : Entity
{
    /// <summary>
    /// The name of the class
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// All students of the class
    /// </summary>
    [InverseProperty("SchoolClass")]
    public ICollection<User> Students { get; set; }
    /// <summary>
    /// All teachers of the class
    /// </summary>
    [InverseProperty("Classes")]
    public ICollection<User> Teachers { get; set; }
}