using digify.Models;
using digify.Shared;

namespace digify.AccessVoter;

/// <summary>
///  Voter used for actions and data validation
/// on the <see cref="Class"/> entity
/// </summary>
public class ClassVoter : IVoter
{
    /// <summary>
    /// If a user can view a <see cref="Class" />
    /// </summary>
    public const string CAN_VIEW = "CAN_VIEW";
    
    /// <summary>
    /// if a user can create a <see cref="Class" />
    /// </summary>
    public const string CAN_CREATE = "CAN_CREATE";

    /// <summary>
    /// If a user can delete a <see cref="Class" />
    /// </summary>
    public const string CAN_DELETE = "CAN_DELETE";
    
    /// <summary>
    /// The <see cref="User"/> that performs the
    /// action that should be validated
    /// </summary>
    private User ActionUser { get; set; }
    
    /// <summary>
    /// The database link that is used for communication
    /// with the main database
    /// </summary>
    private IContext DatabaseRepository { get; set; }
    
    /// <summary>
    /// The class that is referenced and is used for the data
    /// validation.
    /// </summary>
    private Class? RelationClass { get; set; }

    public ClassVoter(Class? relationClass, IContext db)
    {
        RelationClass = relationClass;
        DatabaseRepository = db;
    }
    
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
            case CAN_VIEW:
                return UserCanViewClass();
            case CAN_CREATE:
            case CAN_DELETE:    
                return UserCanCreateOrDeleteClass();
            default:
                return false;
        }
    }

    /// <summary>
    /// Checks if the given user can view the given class
    /// </summary>
    /// <returns>If the user can view the given class</returns>
    private bool UserCanViewClass()
    {
        return DatabaseRepository.Classes
            .FirstOrDefault(c => c.Teachers.Contains(ActionUser) || c.Students.Contains(ActionUser)) != null;
    }

    /// <summary>
    /// Checks if a user is allowed to create a class
    /// </summary>
    /// <returns>If the user can create a class</returns>
    private bool UserCanCreateOrDeleteClass()
    {
        return ActionUser.Roles.Contains(UserRoles.ADMIN);
    }
}