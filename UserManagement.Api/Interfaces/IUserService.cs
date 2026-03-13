using UserManagement.Api.Dtos;

namespace UserManagement.Api.Interfaces;

/// <summary>
/// Defines core business operations for user management.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Retrieves all users in the system.
    /// </summary>
    Task<List<UserDto>> GetAllUsersAsync();

    /// <summary>
    /// Retrieves only active users, sorted by name.
    /// </summary>
    Task<List<UserDto>> GetActiveUsersAsync();

    /// <summary>
    /// Gets a single user by ID.
    /// </summary>
    /// <returns>The user DTO or null if not found.</returns>
    Task<UserDto?> GetUserByIdAsync(Guid id);

    /// <summary>
    /// Attempts to change a user's active status.
    /// </summary>
    /// <remarks>Admins cannot be disabled (business rule).</remarks>
    /// <returns>True if the update was successful.</returns>
    Task<bool> UpdateUserStatusAsync(Guid id, bool active);
}