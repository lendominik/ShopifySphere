using AutoMapper;
using FluentAssertions;
using MediatR;
using Moq;
using Shop.Application.ApplicationUser;
using Shop.Application.Category.Commands.DeleteCategory;
using Shop.Application.Exceptions;
using Shop.Application.Services;
using Shop.Domain.Interfaces;
using Xunit;

namespace Shop.Application.Category.Commands.EditCategory.Tests
{
    public class EditCategoryCommandHandlerTests
    {
        [Fact]
        public async Task Handle_EditCategory_WhenUserIsNotAuthorized()
        {
            // Arrange
            var command = new EditCategoryCommand()
            {
                EncodedName = "categoryname"
            };

            var userContextMock = new Mock<IUserContext>();

            var mapperMock = new Mock<IMapper>();

            userContextMock.Setup(c => c.GetCurrentUser())
                .Returns(new CurrentUser("1", "test@test.com", new[] { "User" }));

            var categoryRepositoryMock = new Mock<ICategoryRepository>();

            var accessControlServiceMock = new Mock<IAccessControlService>();

            accessControlServiceMock.Setup(c => c.IsEditable(userContextMock.Object))
                .Returns(false);

            var handler = new EditCategoryCommandHandler(categoryRepositoryMock.Object, accessControlServiceMock.Object, mapperMock.Object, userContextMock.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().Be(Unit.Value);
            categoryRepositoryMock.Verify(m => m.Commit(), Times.Never);
        }

        [Fact]
        public async Task Handle_EditCategory_WhenUserIsAuthorized()
        {
            // Arrange
            var command = new EditCategoryCommand()
            {
                EncodedName = "categoryname",
                Description = "newdescription"
            };

            var userContextMock = new Mock<IUserContext>();

            var mapperMock = new Mock<IMapper>();

            userContextMock.Setup(c => c.GetCurrentUser())
                .Returns(new CurrentUser("1", "test@test.com", new[] { "User" }));

            var categoryRepositoryMock = new Mock<ICategoryRepository>();

            var accessControlServiceMock = new Mock<IAccessControlService>();

            accessControlServiceMock.Setup(c => c.IsEditable(userContextMock.Object))
                .Returns(true);

            var handler = new EditCategoryCommandHandler(categoryRepositoryMock.Object, accessControlServiceMock.Object, mapperMock.Object, userContextMock.Object);

            categoryRepositoryMock.Setup(s => s.GetByEncodedName("categoryname"))
                .ReturnsAsync(new Domain.Entities.Category
                {
                    Description = "description",
                    EncodedName = "categoryname",
                    Id = 1
                });

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().Be(Unit.Value);
            categoryRepositoryMock.Verify(m => m.Commit(), Times.Once);
        }
        [Fact]
        public async Task Handle_EditCategory_WhenCategoryIsNotFound()
        {
            // Arrange
            var command = new EditCategoryCommand()
            {
                EncodedName = "notfound",
                Description = "newdescription"
            };

            var userContextMock = new Mock<IUserContext>();

            var mapperMock = new Mock<IMapper>();

            userContextMock.Setup(c => c.GetCurrentUser())
                .Returns(new CurrentUser("1", "test@test.com", new[] { "User" }));

            var categoryRepositoryMock = new Mock<ICategoryRepository>();

            var accessControlServiceMock = new Mock<IAccessControlService>();

            accessControlServiceMock.Setup(c => c.IsEditable(userContextMock.Object))
                .Returns(true);

            var handler = new EditCategoryCommandHandler(categoryRepositoryMock.Object, accessControlServiceMock.Object, mapperMock.Object, userContextMock.Object);

            // Act
            var subject = async () => await handler.Handle(command, CancellationToken.None);

            // Assert
            await subject.Should().ThrowAsync<NotFoundException>();
            categoryRepositoryMock.Verify(m => m.Commit(), Times.Never);
        }
    }
}