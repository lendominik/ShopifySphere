using FluentAssertions;
using MediatR;
using Moq;
using Shop.Application.ApplicationUser;
using Shop.Application.Exceptions;
using Shop.Application.Services;
using Shop.Domain.Interfaces;
using Xunit;

namespace Shop.Application.Item.Commands.DeleteItem.Tests
{
    public class DeleteItemCommandHandlerTests
    {
        [Fact]
        public async Task Handle_DeleteItem_WhenUserIsNotAuthorized()
        {
            // Arrange
            var command = new DeleteItemCommand()
            {
                EncodedName = "itemname"
            };

            var userContextMock = new Mock<IUserContext>();

            userContextMock.Setup(c => c.GetCurrentUser())
                .Returns(new CurrentUser("1", "test@test.com", new[] { "User" }));

            var itemRepositoryMock = new Mock<IItemRepository>();

            var accessControlServiceMock = new Mock<IAccessControlService>();

            accessControlServiceMock.Setup(c => c.IsEditable(userContextMock.Object))
                .Returns(false);

            var handler = new DeleteItemCommandHandler(accessControlServiceMock.Object, itemRepositoryMock.Object, userContextMock.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().Be(Unit.Value);
            itemRepositoryMock.Verify(m => m.Delete(It.IsAny<Domain.Entities.Item>()), Times.Never);
        }
        [Fact]
        public async Task Handle_DeleteItem_WhenUserIsAuthorized()
        {
            // Arrange
            var command = new DeleteItemCommand()
            {
                EncodedName = "itemname"
            };

            var userContextMock = new Mock<IUserContext>();

            userContextMock.Setup(c => c.GetCurrentUser())
                .Returns(new CurrentUser("1", "test@test.com", new[] { "User" }));

            var itemRepositoryMock = new Mock<IItemRepository>();
            itemRepositoryMock.Setup(m => m.GetByEncodedName(command.EncodedName))
                .ReturnsAsync(new Domain.Entities.Item
                {
                    Id = 1
                });

            var accessControlServiceMock = new Mock<IAccessControlService>();

            accessControlServiceMock.Setup(c => c.IsEditable(userContextMock.Object))
                .Returns(true);

            var handler = new DeleteItemCommandHandler(accessControlServiceMock.Object, itemRepositoryMock.Object, userContextMock.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().Be(Unit.Value);
            itemRepositoryMock.Verify(m => m.Delete(It.IsAny<Domain.Entities.Item>()), Times.Once);
        }
        [Fact]
        public async Task Handle_DeleteItem_WhenItemIsNotFound()
        {
            // Arrange
            var command = new DeleteItemCommand()
            {
                EncodedName = "itemname"
            };

            var userContextMock = new Mock<IUserContext>();

            userContextMock.Setup(c => c.GetCurrentUser())
                .Returns(new CurrentUser("1", "test@test.com", new[] { "User" }));

            var itemRepositoryMock = new Mock<IItemRepository>();

            var accessControlServiceMock = new Mock<IAccessControlService>();

            accessControlServiceMock.Setup(c => c.IsEditable(userContextMock.Object))
                .Returns(true);

            var handler = new DeleteItemCommandHandler(accessControlServiceMock.Object, itemRepositoryMock.Object, userContextMock.Object);

            // Act
            var subject = async () => await handler.Handle(command, CancellationToken.None);

            // Assert
            await subject.Should().ThrowAsync<NotFoundException>();
        }
    }
}