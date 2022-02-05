using digify.Models;

namespace digify.AccessVoter;

public class UserVoter : IVoter
{
    public const string CREATE_USER = "CREATE_USER";
    public const string ALL_USERS = "ALL_USERS";

    private User ActionUser { get; set; }

    public void SetActionUser(User user)
    {
        ActionUser = user;
    }

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

    private bool UserCanCreateUser()
    {
        return ActionUser.Roles.Contains(User.ROLE_ADMIN);
    }
}