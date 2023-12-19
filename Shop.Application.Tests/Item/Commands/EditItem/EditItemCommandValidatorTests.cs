using Xunit;
using Shop.Application.Item.Commands.EditItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Application.Item.Commands.EditItem;
using FluentValidation.TestHelper;

namespace Shop.Application.Item.Commands.EditItem.Tests
{
    public class EditItemCommandValidatorTests
    {
        [Fact()]
        public void Validate_WithValidCommand_ShouldNotHaveValidationError()
        {
            // Assert
            var validator = new EditItemCommandValidator();

            var command = new EditItemCommand()
            {
                Description = "description",
                Price = 100,
                StockQuantity = 1
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
            var validator = new EditItemCommandValidator();

            var command = new EditItemCommand()
            {
                Description = "desc",
            };

            // Act
            var result = validator.TestValidate(command);

            // Arrange
            result.ShouldHaveValidationErrorFor(i => i.Description);
            result.ShouldHaveValidationErrorFor(i => i.Price);
            result.ShouldHaveValidationErrorFor(i => i.StockQuantity);
        }
    }
}