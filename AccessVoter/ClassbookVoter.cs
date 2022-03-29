﻿using digify.Models;
using digify.Shared;

namespace digify.AccessVoter;

public class ClassbookVoter : IVoter
{

    /// <summary>
    /// If the current logged-in user is a student and is allowed
    /// to view his own classbook.
    /// </summary>
    public const string GET_OWN_CLASSBOOK = "GET_OWN_CLASSBOOK";

    /// <summary>
    /// If the current user is allowed to update a lesson of a classbook
    /// </summary>
    public const string UPDATE_LESSON = "UPDATE_LESSON";

    /// <summary>
    /// If the current user is allowed to add a missing person
    /// </summary>
    public const string ADD_MISSING_PERSON = "ADD_MISSING_PERSON";

    /// <summary>
    /// If the current user is allowed to remove a missing person
    /// </summary>
    public const string REMOVE_MISSING_PERSON = "REMOVE_MISSING_PERSON";

    /// <summary>
    /// If the current user is allowed to update the notes of a day entry.
    /// </summary>
    public const string UPDATE_NOTES = "UPDATE_NOTES";
    
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
    
    public ClassbookVoter(IContext db)
    {
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
            case GET_OWN_CLASSBOOK:
                return ActionUser.Roles.Contains(UserRoles.STUDENT);
            case UPDATE_LESSON:
            case ADD_MISSING_PERSON:
            case REMOVE_MISSING_PERSON:
            case UPDATE_NOTES:    
                return ActionUser.Roles.Contains(UserRoles.TEACHER) || UserIsAdmin();
            default:
                return false;
        }
    }

    /// <summary>
    /// Checks if the current user is an administrator
    /// </summary>
    /// <returns>If the user is an admin</returns>
    private bool UserIsAdmin() => ActionUser.Roles.Contains(UserRoles.ADMIN);
}