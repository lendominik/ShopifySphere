using AutoMapper;
using FluentAssertions;
using MediatR;
using Moq;
using Shop.Application.ApplicationUser;
using Shop.Application.Category.Commands.EditCategory;
using Shop.Application.Exceptions;
using Shop.Application.Services;
using Shop.Domain.Interfaces;
using Xunit;

namespace Shop.Application.Item.Commands.EditItem.Tests
{
    public class EditItemCommandHandlerTests
    {
        [Fact]
        public async Task Handle_EditItem_WhenUserIsNotAuthorized()
        {
            // Arrange
            var command = new EditItemCommand()
            {
                EncodedName = "itemname"
            };

            var userContextMock = new Mock<IUserContext>();

            var mapperMock = new Mock<IMapper>();

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
                .Returns(false);

            var handler = new EditItemCommandHandler(userContextMock.Object, itemRepositoryMock.Object, accessControlServiceMock.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().Be(Unit.Value);
            itemRepositoryMock.Verify(m => m.Commit(), Times.Never);
        }
        [Fact]
        public async Task Handle_EditItem_WhenUserIsAuthorized()
        {
            // Arrange
            var command = new EditItemCommand()
            {
                EncodedName = "itemname"
            };

            var userContextMock = new Mock<IUserContext>();

            var mapperMock = new Mock<IMapper>();

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

            var handler = new EditItemCommandHandler(userContextMock.Object, itemRepositoryMock.Object, accessControlServiceMock.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().Be(Unit.Value);
            itemRepositoryMock.Verify(m => m.Commit(), Times.Once);
        }
        [Fact]
        public async Task Handle_EditItem_WhenItemIsNotFound()
        {
            // Arrange
            var command = new EditItemCommand()
            {
                EncodedName = "itemname"
            };

            var userContextMock = new Mock<IUserContext>();

            var mapperMock = new Mock<IMapper>();

            userContextMock.Setup(c => c.GetCurrentUser())
                .Returns(new CurrentUser("1", "test@test.com", new[] { "User" }));

            var itemRepositoryMock = new Mock<IItemRepository>();

            var accessControlServiceMock = new Mock<IAccessControlService>();

            accessControlServiceMock.Setup(c => c.IsEditable(userContextMock.Object))
                .Returns(true);

            var handler = new EditItemCommandHandler(userContextMock.Object, itemRepositoryMock.Object, accessControlServiceMock.Object);

            // Act
            var subject = async () => await handler.Handle(command, CancellationToken.None);

            // Assert
            await subject.Should().ThrowAsync<NotFoundException>();
        }
    }
}