using UserManagement.Common.Models;

namespace UserManagement.Maui.Services;

public interface IUserApiService
{
    Task<List<User>> GetUsersAsync();
    Task<bool> UpdateUserStatusAsync(Guid id, bool active);
}
