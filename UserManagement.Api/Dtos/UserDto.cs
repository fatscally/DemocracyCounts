namespace UserManagement.Api.Dtos;

/// <summary>
/// Data transfer object for returning user information to clients.
/// </summary>
public class UserDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool Active { get; set; }
    public List<string> Roles { get; set; } = new();
}

/// <summary>
/// DTO for requests to change a user's active status.
/// </summary>
public class UpdateUserStatusDto
{
    /// <summary>
    /// Desired active state (true = enable, false = disable).
    /// </summary>
    public bool Active { get; set; }
}