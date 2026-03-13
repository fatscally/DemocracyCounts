using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using UserManagement.Common.Models;

namespace UserManagement.Maui.Services;

public class UserApiService : IUserApiService
{
    private readonly HttpClient _httpClient;

    public UserApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://localhost:7238/"); // ← change or use config
    }

    public async Task<List<User>> GetUsersAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<List<User>>("api/users");
        return response ?? new();
    }

    public async Task<bool> UpdateUserStatusAsync(Guid id, bool active)
    {
        var content = new StringContent(
            JsonSerializer.Serialize(new { active }),
            Encoding.UTF8,
            "application/json");

        var response = await _httpClient.PutAsync($"api/users/{id}/status", content);
        return response.IsSuccessStatusCode;
    }
}
