using Xunit;
using AutoMapper;
using MediatR;
using Moq;
using Shop.Application.ApplicationUser;
using Shop.Application.Services;
using Shop.Domain.Interfaces;
using Microsoft.AspNetCore.Hosting;
using FluentAssertions;
using Microsoft.AspNetCore.Http;

namespace Shop.Application.Item.Commands.CreateItem.Tests
{
    public class CreateItemCommandHandlerTests
    {
        [Fact]
        public async Task Handle_CreatesItem_WhenUserIsAuthorized()
        {
            // Arrange

            var category = new Domain.Entities.Category
            {
                Description = "description",
                Name = "name",
                Id = 1,
                EncodedName = "categoryname"
            };

            var command = new CreateItemCommand()
            {
                Description = "description",
                EncodedName = "itemname",
                Name = "name",
                Category = category,
                CategoryEncodedName = category.EncodedName,
                CategoryId = category.Id,
                Price = 10,
                StockQuantity = 10,
                ProductImage = "img"
            };

            var userContextMock = new Mock<IUserContext>();

            userContextMock.Setup(c => c.GetCurrentUser())
                .Returns(new CurrentUser("1", "test@test.com", new[] { "Owner" }));

            var categoryRepositoryMock = new Mock<ICategoryRepository>();
            categoryRepositoryMock.Setup(m => m.GetByEncodedName("categoryname"))
                .ReturnsAsync(category);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<Domain.Entities.Item>(command))
                .Returns(new Domain.Entities.Item
                {
                    Description = command.Description,
                    EncodedName = command.EncodedName,
                    Name = command.Name,
                    Category = command.Category,
                    CategoryId = command.CategoryId,
                    Price = command.Price,
                    ProductImage = command.ProductImage,
                    StockQuantity = command.StockQuantity,
                    Id = 1
                });

            var formFileMock = new Mock<IFormFile>();

            var webHostEnvironmentMock = new Mock<IWebHostEnvironment>();
            
            var itemRepositoryMock = new Mock<IItemRepository>();

            var accessControlService = new Mock<IAccessControlService>();
            accessControlService.Setup(c => c.IsEditable(userContextMock.Object))
                .Returns(true);

            var fileServiceMock = new Mock<IFileService>();
            fileServiceMock.Setup(m => m.UploadFile(formFileMock.Object, webHostEnvironmentMock.Object))
                .Returns("filename");

            var handler = new CreateItemCommandHandler(userContextMock.Object, webHostEnvironmentMock.Object, accessControlService.Object, fileServiceMock.Object, itemRepositoryMock.Object, categoryRepositoryMock.Object, mapperMock.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().Be(Unit.Value);
            itemRepositoryMock.Verify(m => m.Create(It.IsAny<Domain.Entities.Item>()), Times.Once);
        }

        [Fact]
        public async Task Handle_CreatesItem_WhenUserIsNotAuthorized()
        {
            // Arrange

            var category = new Domain.Entities.Category
            {
                Description = "description",
                Name = "name",
                Id = 1,
                EncodedName = "categoryname"
            };

            var command = new CreateItemCommand()
            {
                Description = "description",
                EncodedName = "itemname",
                Name = "name",
                Category = category,
                CategoryEncodedName = category.EncodedName,
                CategoryId = category.Id,
                Price = 10,
                StockQuantity = 10,
                ProductImage = "img"
            };

            var userContextMock = new Mock<IUserContext>();

            userContextMock.Setup(c => c.GetCurrentUser())
                .Returns(new CurrentUser("1", "test@test.com", new[] { "Owner" }));

            var categoryRepositoryMock = new Mock<ICategoryRepository>();
            categoryRepositoryMock.Setup(m => m.GetByEncodedName("categoryname"))
                .ReturnsAsync(category);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<Domain.Entities.Item>(command))
                .Returns(new Domain.Entities.Item
                {
                    Description = command.Description,
                    EncodedName = command.EncodedName,
                    Name = command.Name,
                    Category = command.Category,
                    CategoryId = command.CategoryId,
                    Price = command.Price,
                    ProductImage = command.ProductImage,
                    StockQuantity = command.StockQuantity,
                    Id = 1
                });

            var formFileMock = new Mock<IFormFile>();

            var webHostEnvironmentMock = new Mock<IWebHostEnvironment>();

            var itemRepositoryMock = new Mock<IItemRepository>();

            var accessControlService = new Mock<IAccessControlService>();
            accessControlService.Setup(c => c.IsEditable(userContextMock.Object))
                .Returns(false);

            var fileServiceMock = new Mock<IFileService>();
            fileServiceMock.Setup(m => m.UploadFile(formFileMock.Object, webHostEnvironmentMock.Object))
                .Returns("filename");

            var handler = new CreateItemCommandHandler(userContextMock.Object, webHostEnvironmentMock.Object, accessControlService.Object, fileServiceMock.Object, itemRepositoryMock.Object, categoryRepositoryMock.Object, mapperMock.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().Be(Unit.Value);
            itemRepositoryMock.Verify(m => m.Create(It.IsAny<Domain.Entities.Item>()), Times.Never);
        }
    }
}