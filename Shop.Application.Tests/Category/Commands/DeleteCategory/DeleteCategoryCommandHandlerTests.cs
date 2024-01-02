using Xunit;
using MediatR;
using Moq;
using Shop.Application.ApplicationUser;
using Shop.Application.Services;
using Shop.Domain.Interfaces;
using FluentAssertions;
using Shop.Application.Exceptions;

namespace Shop.Application.Category.Commands.DeleteCategory.Tests
{
    public class DeleteCategoryCommandHandlerTests
    {
        [Fact]
        public async Task Handle_DeleteCategory_WhenUserIsNotAuthorized()
        {
            // Arrange
            var command = new DeleteCategoryCommand()
            {
                EncodedName = "categoryname"
            };

            var userContextMock = new Mock<IUserContext>();

            userContextMock.Setup(c => c.GetCurrentUser())
                .Returns(new CurrentUser("1", "test@test.com", new[] { "User" }));

            var categoryRepositoryMock = new Mock<ICategoryRepository>();

            var accessControlServiceMock = new Mock<IAccessControlService>();

            accessControlServiceMock.Setup(c => c.IsEditable(userContextMock.Object))
                .Returns(false);

            var handler = new DeleteCategoryCommandHandler(accessControlServiceMock.Object, categoryRepositoryMock.Object, userContextMock.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().Be(Unit.Value);
            categoryRepositoryMock.Verify(m => m.Delete(It.IsAny<Domain.Entities.Category>()), Times.Never);
        }
        [Fact()]
        public async Task Handle_DeleteCategory_WhenUserIsAuthorized()
        {
            // Arrange
            var command = new DeleteCategoryCommand()
            {
                EncodedName = "categoryname"
            };

            var userContextMock = new Mock<IUserContext>();

            userContextMock.Setup(c => c.GetCurrentUser())
                .Returns(new CurrentUser("1", "test@test.com", new[] { "User" }));

            var categoryRepositoryMock = new Mock<ICategoryRepository>();

            categoryRepositoryMock.Setup(s => s.GetByEncodedName("categoryname"))
                .ReturnsAsync(new Domain.Entities.Category
                {
                    Description = "description",
                    EncodedName = "categoryname",
                    Id = 1
                });

            var accessControlServiceMock = new Mock<IAccessControlService>();

            accessControlServiceMock.Setup(c => c.IsEditable(userContextMock.Object))
                .Returns(true);

            var handler = new DeleteCategoryCommandHandler(accessControlServiceMock.Object, categoryRepositoryMock.Object, userContextMock.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().Be(Unit.Value);
            categoryRepositoryMock.Verify(m => m.Delete(It.IsAny<Domain.Entities.Category>()), Times.Once);
        }
        [Fact()]
        public async Task Handle_DeleteCategory_WhenCategoryIsNotFound()
        {
            // Arrange
            var command = new DeleteCategoryCommand()
            {
                EncodedName = "categoryname"
            };

            var userContextMock = new Mock<IUserContext>();

            userContextMock.Setup(c => c.GetCurrentUser())
                .Returns(new CurrentUser("1", "test@test.com", new[] { "User" }));

            var categoryRepositoryMock = new Mock<ICategoryRepository>();

            var accessControlServiceMock = new Mock<IAccessControlService>();

            accessControlServiceMock.Setup(c => c.IsEditable(userContextMock.Object))
                .Returns(true);

            var handler = new DeleteCategoryCommandHandler(accessControlServiceMock.Object, categoryRepositoryMock.Object, userContextMock.Object);

            // Act
            var subject = async () => await handler.Handle(command, CancellationToken.None);

            // Assert
            await subject.Should().ThrowAsync<NotFoundException>();
        }
    }
}