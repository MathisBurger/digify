using digify.Models;

namespace digify.AccessVoter;

public interface IVoter
{
    bool VoteOnAttribute(string action);
    void SetActionUser(User user);
}