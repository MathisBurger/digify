using digify.Models;

namespace digify.AccessVoter;

/// <summary>
/// The voter that is used for validating user specific
/// requests.
/// </summary>
public class UserVoter : IVoter
{
    /// <summary>
    /// If a user can create a new user
    /// </summary>
    public const string CREATE_USER = "CREATE_USER";
    /// <summary>
    /// If a user can view all users
    /// </summary>
    public const string ALL_USERS = "ALL_USERS";

    /// <summary>
    /// The <see cref="User"/> that performs the
    /// action that should be validated
    /// </summary>
    private User ActionUser { get; set; }

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
            case ALL_USERS:
            case CREATE_USER:
                return UserCanCreateUser();
            default:
                return false;
        }
    }

    /// <summary>
    /// Checks if the current user can create new users
    /// </summary>
    /// <returns>If the user can create a new user</returns>
    private bool UserCanCreateUser()
    {
        return ActionUser.Roles.Contains(UserRoles.ADMIN);
    }
}