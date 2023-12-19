using FluentAssertions;
using Moq;
using Shop.Application.ApplicationUser;
using Xunit;

namespace Shop.Application.Services.Tests
{
    public class AccessControlServiceTests
    {
        [Fact]
        public void IsEditable_ReturnsTrueForOwnerRole_ReturnsTrue()
        {
            // Arrange
            var currentUser = new CurrentUser("1", "test@tests.com", new List<string> { "Owner" });
            var userContextMock = new Mock<IUserContext>();
            userContextMock.Setup(u => u.GetCurrentUser()).Returns(currentUser);

            var accessControlService = new AccessControlService(userContextMock.Object);

            // Act
            var isEditable = accessControlService.IsEditable();

            // Assert
            isEditable.Should().BeTrue();
        }
        [Fact]
        public void IsEditable_ReturnsTrueForOwnerRole_ReturnsFalse()
        {
            // Arrange
            var currentUser = new CurrentUser("1", "test@tests.com", new List<string> { "Admin" });
            var userContextMock = new Mock<IUserContext>();
            userContextMock.Setup(u => u.GetCurrentUser()).Returns(currentUser);

            var accessControlService = new AccessControlService(userContextMock.Object);

            // Act
            var isEditable = accessControlService.IsEditable();

            // Assert
            isEditable.Should().BeFalse();
        }
    }
}