namespace digify.Models;

public class Class : Entity
{
    
    public string Name { get; set; }
    public ICollection<User> Teachers { get; set; }
    public ICollection<User> Students { get; set; }
}