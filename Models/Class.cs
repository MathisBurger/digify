﻿using System.Collections.ObjectModel;

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
    public ICollection<User> Students { get; set; }
    /// <summary>
    /// All teachers of the class
    /// </summary>
    public IList<TeacherClass> Teachers { get; set; }
}