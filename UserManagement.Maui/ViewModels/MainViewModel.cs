using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using UserManagement.Common.Models;
using UserManagement.Maui.Services;

namespace UserManagement.Maui.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly IUserApiService _apiService;

    [ObservableProperty]
    private ObservableCollection<User> users = new();

    [ObservableProperty] private string emptyMessage = "Empty";


    public MainViewModel(IUserApiService apiService)
    {
        _apiService = apiService;
        LoadUsersCommand.Execute(null);
    }

    [RelayCommand]
    private async Task LoadUsers()
    {
        try
        {
            EmptyMessage = "Searching...";

            var data = await _apiService.GetUsersAsync();
            Users = new ObservableCollection<User>(data);
        }
        catch (Exception ex)
        {
            EmptyMessage = "Fiddlesticks, no users found.";

            Debug.WriteLine($"Error loading users: {ex.Message}");
            Debug.WriteLine($"Inner Exception: {ex.InnerException}");
        }
    }

    [RelayCommand]
    private async Task ToggleActive(User user)
    {
        if (user == null) return;

        var success = await _apiService.UpdateUserStatusAsync(user.Id, !user.Active);
        if (success)
        {
            user.Active = !user.Active;
        }
        else
        {
            await Shell.Current.DisplayAlert("Error", "Could not update status", "OK");
        }
    }
}
