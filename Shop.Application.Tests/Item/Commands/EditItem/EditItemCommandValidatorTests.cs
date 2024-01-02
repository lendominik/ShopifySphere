using Xunit;
using Microsoft.AspNetCore.Http;
using Moq;
using FluentValidation.TestHelper;

namespace Shop.Application.Item.Commands.EditItem.Tests
{
    public class EditItemCommandValidatorTests
    {
        [Fact]
        public async void Validate_WithValidCommand_ShouldNotHaveValidationError()
        {
            // Arrange
            var validator = new EditItemCommandValidator();
            var formFile = new Mock<IFormFile>();
            var command = new EditItemCommand()
            {
                Description = "description",
                EncodedName = "itemname",
                Image = formFile.Object,
                Price = 10,
                StockQuantity = 1
            };

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
        [Fact]
        public async void Validate_WithValidCommand_ShouldHaveValidationError()
        {
            // Arrange
            var validator = new EditItemCommandValidator();
            var formFile = new Mock<IFormFile>();
            var command = new EditItemCommand()
            {
                Description = "abc",
                Image = formFile.Object,
            };

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.Description);
            result.ShouldHaveValidationErrorFor(c => c.Price);
            result.ShouldHaveValidationErrorFor(c => c.StockQuantity);
        }
    }
}