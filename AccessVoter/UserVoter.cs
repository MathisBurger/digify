using digify.Models;

namespace digify.AccessVoter;

public class UserVoter : IVoter
{
    public const string CREATE_USER = "CREATE_USER";

    private User ActionUser { get; set; }

    public void SetActionUser(User user)
    {
        ActionUser = user;
    }

    public bool VoteOnAttribute(string action)
    {
        switch (action)
        {
            case CREATE_USER:
                return this.userCanCreateUser();
            default:
                return false;
        }
    }

    private bool userCanCreateUser()
    {
        return ActionUser.Roles.Contains(User.ROLE_ADMIN);
    }
}