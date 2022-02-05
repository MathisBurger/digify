using digify.Models;
using digify.Shared;

namespace digify.AccessVoter;

public class ClassVoter : IVoter
{
    public const string CAN_VIEW = "CAN_VIEW";
    
    private User ActionUser { get; set; }
    private IContext DatabaseRepository { get; set; }
    private Class RelationClass { get; set; }

    public ClassVoter(Class relationClass, IContext db)
    {
        RelationClass = relationClass;
        DatabaseRepository = db;
    }
    
    public void SetActionUser(User user)
    {
        ActionUser = user;
    }

    public bool VoteOnAttribute(string action)
    {
        switch (action)
        {
            case CAN_VIEW:
                return UserCanViewClass();
            default:
                return false;
        }
    }

    private bool UserCanViewClass()
    {
        return DatabaseRepository.Classes
                .Where(
                    c => c.Teachers.Contains(ActionUser)
                    ).FirstOrDefault() != null
               || DatabaseRepository.Users.Where(u => u.schoolClass != null && u.schoolClass.Id == RelationClass.Id).FirstOrDefault() != null;
    }
}