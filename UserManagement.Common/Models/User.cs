namespace UserManagement.Common.Models;

/// <summary>
/// Represents a user in the system with basic profile and status information.
/// </summary>
public class User
{

    public Guid Id { get; set; } = Guid.Empty;

    /// <summary>
    /// Full name of the user.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Indicates whether the user account is currently active.
    /// </summary>
    public bool Active { get; set; } = true;

    /// <summary>
    /// List of roles assigned to the user (e.g., "Admin", "User").
    /// </summary>
    public List<string> Roles { get; set; } = new();

    public string RolesDisplay => Roles != null && Roles.Count > 0 ? string.Join(", ", Roles) : "None";

    /// <summary>
    /// Gets a value indicating whether the user has the "Admin" role.
    /// </summary>
    /// <remarks>
    /// This is a computed property based on the Roles collection.
    /// Case-insensitive comparison is used for robustness.
    /// </remarks>
    public bool IsAdmin =>
        Roles?.Any(r => string.Equals(r?.Trim(), "Admin", StringComparison.OrdinalIgnoreCase)) == true;

}