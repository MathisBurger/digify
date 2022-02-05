using System.ComponentModel;
using System.Runtime.Serialization;

namespace digify.Models;

/// <summary>
/// All roles a user can have
/// </summary>
public static class UserRoles
{
    /// <summary>
    /// The administrator is allowed to do everything
    /// </summary>
    public const string ADMIN = "ADMIN";
    /// <summary>
    /// A teacher has elevated rights and is allowed
    /// to manage classes he is assigned to.
    /// </summary>
    public const string TEACHER = "TEACHER";
    /// <summary>
    /// The student is just allowed to view data
    /// </summary>
    public const string STUDENT = "STUDENT";
}