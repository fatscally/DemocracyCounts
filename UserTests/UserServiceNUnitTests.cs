using NUnit.Framework;
using UserManagement.Api.Services;

namespace UserManagement.Tests.Services;

[TestFixture]
public class UserServiceNUnitTests
{
  [Test]
  public async Task GetAllUsersAsync_ShouldReturnAllSeededUsers()
  {
    // Arrange
    var service = new UserService();

    // Act
    var result = await service.GetAllUsersAsync();

    // Assert
    Assert.That(result, Is.Not.Null);
    Assert.That(result.Count, Is.EqualTo(10));
    Assert.That(result.Any(u => u.Name == "Anna"), Is.True);
    Assert.That(result.Any(u => u.Name == "Jane"), Is.True);
  }

  [Test]
  public async Task GetActiveUsersAsync_ShouldReturnOnlyActiveUsersOrderedByName()
  {
    // Arrange
    var service = new UserService();

    // Act
    var result = await service.GetActiveUsersAsync();

    // Assert
    Assert.That(result, Is.Not.Null);
    Assert.That(result.Count, Is.EqualTo(6));
    Assert.That(result.All(u => u.Active), Is.True);

    var orderedNames = result.Select(u => u.Name).ToList();
    var expectedNames = orderedNames.OrderBy(n => n).ToList();

    Assert.That(orderedNames, Is.EqualTo(expectedNames));
  }

  [Test]
  public async Task GetUserByIdAsync_ShouldReturnUser_WhenUserExists()
  {
    // Arrange
    var service = new UserService();
    var existingUser = (await service.GetAllUsersAsync()).First();

    // Act
    var result = await service.GetUserByIdAsync(existingUser.Id);

    // Assert
    Assert.That(result, Is.Not.Null);
    Assert.That(result!.Id, Is.EqualTo(existingUser.Id));
    Assert.That(result.Name, Is.EqualTo(existingUser.Name));
    Assert.That(result.Email, Is.EqualTo(existingUser.Email));
  }

  [Test]
  public async Task GetUserByIdAsync_ShouldReturnNull_WhenUserDoesNotExist()
  {
    // Arrange
    var service = new UserService();
    var nonExistentId = Guid.NewGuid();

    // Act
    var result = await service.GetUserByIdAsync(nonExistentId);

    // Assert
    Assert.That(result, Is.Null);
  }

  [Test]
  public async Task UpdateUserStatusAsync_ShouldReturnFalse_WhenUserDoesNotExist()
  {
    // Arrange
    var service = new UserService();

    // Act
    var result = await service.UpdateUserStatusAsync(Guid.NewGuid(), false);

    // Assert
    Assert.That(result, Is.False);
  }



  [Test]
  public async Task UpdateUserStatusAsync_ShouldUpdateStatus_WhenUserIsNotAdmin()
  {
    // Arrange
    var service = new UserService();
    var normalUser = (await service.GetAllUsersAsync())
        .First(u => u.Roles.Contains("User"));

    // Act
    var result = await service.UpdateUserStatusAsync(normalUser.Id, false);
    var updatedUser = await service.GetUserByIdAsync(normalUser.Id);

    // Assert
    Assert.That(result, Is.True);
    Assert.That(updatedUser, Is.Not.Null);
    Assert.That(updatedUser!.Active, Is.False);
  }
}