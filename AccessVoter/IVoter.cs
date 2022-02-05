using digify.Models;

namespace digify.AccessVoter;

/// <summary>
/// The interface that constructs every AccessVoter
/// </summary>
public interface IVoter
{
    /// <summary>
    /// Checks if the current logged in user
    /// is allowed to perform the action with the given key
    /// </summary>
    /// <param name="action">The key of the action that should be performed</param>
    /// <returns></returns>
    bool VoteOnAttribute(string action);
    /// <summary>
    /// Sets the user that wants to perform this action
    /// </summary>
    /// <param name="user">The user that performs this action</param>
    void SetActionUser(User user);
}