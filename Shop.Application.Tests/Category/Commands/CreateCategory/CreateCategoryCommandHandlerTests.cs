using Xunit;
using Moq;
using Shop.Application.ApplicationUser;
using Shop.Domain.Interfaces;
using AutoMapper;
using Shop.Application.Services;
using FluentAssertions;
using MediatR;

namespace Shop.Application.Category.Commands.CreateCategory.Tests
{
    public class CreateCategoryCommandHandlerTests
    {
        [Fact()]
        public async Task Handle_CreatesCategory_WhenUserIsAuthorized()
        {
            // Arrange
            var command = new CreateCategoryCommand()
            {
                Description = "description",
                EncodedName = "categoryname",
                Name = "name",
            };

            var userContextMock = new Mock<IUserContext>();

            userContextMock.Setup(c => c.GetCurrentUser())
                .Returns(new CurrentUser("1","test@test.com", new[] { "Owner" }));

            var categoryRepositoryMock = new Mock<ICategoryRepository>();

            var mapperMock = new Mock<IMapper>();

            mapperMock.Setup(m => m.Map<Domain.Entities.Category>(command))
                .Returns(new Domain.Entities.Category
                {
                    Description = command.Description,
                    EncodedName= command.EncodedName,
                    Items = command.Items,
                    Name = command.Name
                });

            var accessControlService = new Mock<IAccessControlService>();

            accessControlService.Setup(c => c.IsEditable(userContextMock.Object))
                .Returns(true);

            var handler = new CreateCategoryCommandHandler(accessControlService.Object, categoryRepositoryMock.Object, mapperMock.Object, userContextMock.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().Be(Unit.Value);
            categoryRepositoryMock.Verify(m => m.Create(It.IsAny<Domain.Entities.Category>()), Times.Once);
        }
        [Fact()]
        public async Task Handle_CreatesCategory_WhenUserIsNotAuthorized()
        {
            // Arrange
            var command = new CreateCategoryCommand()
            {
                Description = "description",
                EncodedName = "categoryname",
                Name = "name",
            };

            var userContextMock = new Mock<IUserContext>();

            userContextMock.Setup(c => c.GetCurrentUser())
                .Returns(new CurrentUser("1", "test@test.com", new[] { "User" }));

            var categoryRepositoryMock = new Mock<ICategoryRepository>();

            var mapperMock = new Mock<IMapper>();

            mapperMock.Setup(m => m.Map<Domain.Entities.Category>(command))
                .Returns(new Domain.Entities.Category
                {
                    Description = command.Description,
                    EncodedName = command.EncodedName,
                    Items = command.Items,
                    Name = command.Name
                });

            var accessControlService = new Mock<IAccessControlService>();

            accessControlService.Setup(c => c.IsEditable(userContextMock.Object))
                .Returns(false);

            var handler = new CreateCategoryCommandHandler(accessControlService.Object, categoryRepositoryMock.Object, mapperMock.Object, userContextMock.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().Be(Unit.Value);
            categoryRepositoryMock.Verify(m => m.Create(It.IsAny<Domain.Entities.Category>()), Times.Never);
        }
    }
}