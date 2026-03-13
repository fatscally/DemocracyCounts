using UserManagement.Api.Dtos;
using UserManagement.Api.Interfaces;
using UserManagement.Common.Models;

namespace UserManagement.Api.Services;


/// <summary>
/// In-memory implementation of user management business logic.
/// </summary>
public class UserService : IUserService
{
    private readonly List<User> _users = new();

    public UserService()
    {
        if (_users.Count == 0)
            SeedData();
    }

    private void SeedData()
    {
        var names = new[] { "Anna", "Brian", "Charlie", "David", "Eric", "Frank", "George", "Hillary", "Isabella", "Jane" };
        var ids = new[] { "00000000-0000-0000-0000-000000000000", "00000000-0000-0000-0000-000000000111", "00000000-0000-0000-0000-000000000222" ,
                          "00000000-0000-0000-0000-000000000333", "00000000-0000-0000-0000-000000000444", "00000000-0000-0000-0000-000000000555" ,
                          "00000000-0000-0000-0000-000000000666", "00000000-0000-0000-0000-000000000777", "00000000-0000-0000-0000-000000000888", 
                          "00000000-0000-0000-0000-000000000999"};
        for (int i = 0; i < names.Length; i++)
        {
            _users.Add(new User
            {
                Id = Guid.Parse(ids[i]),
                Name = names[i],
                Email = $"{names[i].ToLower()}@email.com",
                Active = i % 3 != 0, // some inactive
                Roles = i == 0 ? new List<string> { "Admin" } : new List<string> { "User" }
            });
        }
    }

    public Task<List<UserDto>> GetAllUsersAsync()
    {
        return Task.FromResult(_users.Select(MapToDto).ToList());
    }

    public Task<List<UserDto>> GetActiveUsersAsync()
    {
        return Task.FromResult(_users
            .Where(u => u.Active)
            .OrderBy(u => u.Name)
            .Select(MapToDto)
            .ToList());
    }

    public Task<UserDto?> GetUserByIdAsync(Guid id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        return Task.FromResult(user != null ? MapToDto(user) : null);
    }

    public Task<bool> UpdateUserStatusAsync(Guid id, bool active)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user == null) return Task.FromResult(false);


        if (!active && user.Roles.Contains("Admin"))
        {
            return Task.FromResult(false); 
        }

        user.Active = active;
        return Task.FromResult(true);
    }

    private static UserDto MapToDto(User user) => new()
    {
        Id = user.Id,
        Name = user.Name,
        Email = user.Email,
        Active = user.Active,
        Roles = user.Roles
    };
}