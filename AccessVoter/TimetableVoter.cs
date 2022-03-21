using digify.Models;

namespace digify.AccessVoter;

public class TimetableVoter : IVoter
{
    public const string READ = "READ";
    public const string ACTION_READ = "ACTION_READ";
    public const string CREATE = "CREATE";
    public const string DELETE = "DELETE";
    public const string UPDATE = "UPDATE";

    private User ActionUser;
    
    public TimetableVoter() {}

    /// <inheritdoc cref="IVoter"/>
    public void SetActionUser(User user)
    {
        ActionUser = user;
    }

    /// <inheritdoc cref="IVoter"/>
    public bool VoteOnAttribute(string action)
    {
        switch (action)
        {
            case READ:
                return ActionUser.Roles.Contains(UserRoles.STUDENT);
            case ACTION_READ:
            case CREATE:
            case DELETE: 
            case UPDATE:    
                return ActionUser.Roles.Contains(UserRoles.ADMIN);
            default:
                return false;
        }
    }
}