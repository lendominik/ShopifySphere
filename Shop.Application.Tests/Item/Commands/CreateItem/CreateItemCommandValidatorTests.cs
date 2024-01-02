using Xunit;
using Moq;
using Shop.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using FluentValidation.TestHelper;

namespace Shop.Application.Item.Commands.CreateItem.Tests
{
    public class CreateItemCommandValidatorTests
    {
        [Fact()]
        public void Validate_WithValidCommand_ShouldNotHaveValidationError()
        {
            // Arrange
            var mockItemRepository = new Mock<IItemRepository>();
            var formFile = new Mock<IFormFile>();
            var validator = new CreateItemCommandValidator(mockItemRepository.Object);

            var category = new Domain.Entities.Category()
            {
               Description = "description",
               Id = 1,
               Name = "name",
               Items = new List<Domain.Entities.Item>(),
               EncodedName = "categoryname"
            };

            var command = new CreateItemCommand()
            {
                Description = "description",
                Image = formFile.Object,
                Name = "name",
                StockQuantity = 1,
                Price = 50,
                ProductImage = "test",
                Category = category,
                CategoryEncodedName = "categoryname",
                CategoryId = 1,
                EncodedName = "itemname"
            };

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
        [Fact()]
        public void Validate_WithValidCommand_ShouldHaveValidationError()
        {
            // Arrange
            var mockItemRepository = new Mock<IItemRepository>();
            var formFile = new Mock<IFormFile>();
            var validator = new CreateItemCommandValidator(mockItemRepository.Object);

            var category = new Domain.Entities.Category()
            {
                Description = "description",
                Id = 1,
                Name = "name",
                Items = new List<Domain.Entities.Item>(),
                EncodedName = "categoryname"
            };

            var command = new CreateItemCommand()
            {
                Description = "des",
                Image = formFile.Object,
                Name = null,
                ProductImage = "test",
                Category = category,
                CategoryEncodedName = "categoryname",
                CategoryId = 1,
                EncodedName = "itemname"
            };

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(r => r.Name);
            result.ShouldHaveValidationErrorFor(r => r.Description);
            result.ShouldHaveValidationErrorFor(r => r.StockQuantity);
            result.ShouldHaveValidationErrorFor(r => r.Price);
        }
    }
}