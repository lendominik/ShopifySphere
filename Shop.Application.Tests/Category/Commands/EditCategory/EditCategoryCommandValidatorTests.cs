using Xunit;
using FluentValidation.TestHelper;

namespace Shop.Application.Category.Commands.EditCategory.Tests
{
    public class EditCategoryCommandValidatorTests
    {
        [Fact()]
        public void Validate_WithValidCommand_ShouldNotHaveValidationError()
        {
            // Assert
            var validator = new EditCategoryCommandValidator();

            var command = new EditCategoryCommand()
            {
                Description = "description",
                EncodedName = "categoryname",
            };

            // Act
            var result = validator.TestValidate(command);

            // Arrange
            result.ShouldNotHaveAnyValidationErrors();
        }
        [Fact()]
        public void Validate_WithValidCommand_ShouldHaveValidationError()
        {
            // Assert
            var validator = new EditCategoryCommandValidator();

            var command = new EditCategoryCommand()
            {
                Description = "desc",
                EncodedName = "categoryname",
            };

            // Act
            var result = validator.TestValidate(command);

            // Arrange
            result.ShouldHaveValidationErrorFor(i => i.Description);
        }
    }
}