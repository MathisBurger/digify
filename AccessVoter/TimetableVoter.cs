using digify.Models;

namespace digify.AccessVoter;

public class TimetableVoter : IVoter
{
    /// <summary>
    /// If a user is allowed to read the timetable
    /// </summary>
    public const string READ = "READ";
    
    /// <summary>
    /// If a user can read the timetable via an fetch action
    /// </summary>
    public const string ACTION_READ = "ACTION_READ";
    
    /// <summary>
    /// If the current user can create a timetable
    /// </summary>
    public const string CREATE = "CREATE";
    
    /// <summary>
    /// If the current user can delete a timetable
    /// </summary>
    public const string DELETE = "DELETE";
    
    /// <summary>
    /// If the current user can update a timetable
    /// </summary>
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