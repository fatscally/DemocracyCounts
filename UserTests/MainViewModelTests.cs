using NSubstitute;
using NUnit.Framework;
using System.Collections.ObjectModel;
using UserManagement.Common.Models;
using UserManagement.Maui.Services;
using UserManagement.Maui.ViewModels;

namespace UserManagement.Maui.Tests;

/// <summary>
/// Unit tests for MainViewModel focusing on command behavior and state changes.
/// </summary>
[TestFixture]
public class MainViewModelTests
{
    private MainViewModel _viewModel = null!;
    private IUserApiService _apiServiceMock = null!;

    [SetUp]
    public void Setup()
    {
        _apiServiceMock = Substitute.For<IUserApiService>();
        _viewModel = new MainViewModel(_apiServiceMock);
    }


    [Test]
    public async Task ToggleActiveCommand_WhenApiCallSucceeds_TogglesUserActiveStatus()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = "Test User",
            Email = "test@example.com",
            Active = true  // initial state
        };

        _viewModel.Users = new ObservableCollection<User> { user };

        // Mock successful API update (toggle to false)
        _apiServiceMock
            .UpdateUserStatusAsync(user.Id, false)
            .Returns(Task.FromResult(true));

        // Act
        await _viewModel.ToggleActiveCommand.ExecuteAsync(user);

        // Assert
        Assert.That(user.Active, Is.False, "User Active should be toggled to false after successful API call");

        await _apiServiceMock.Received(1).UpdateUserStatusAsync(user.Id, false);
    }


}