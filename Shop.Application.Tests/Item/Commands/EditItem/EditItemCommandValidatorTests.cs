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
            var validator = new EditItemCommandValidator();

            var command = new EditItemCommand()
            {
                Description = "description",
                Price = 100,
                StockQuantity = 1
            };

            var result = validator.TestValidate(command);

            result.ShouldNotHaveAnyValidationErrors();
        }
        [Fact()]
        public void Validate_WithValidCommand_ShouldHaveValidationError()
        {
            var validator = new EditItemCommandValidator();

            var command = new EditItemCommand()
            {
                Description = "desc",
            };

            var result = validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(i => i.Description);
            result.ShouldHaveValidationErrorFor(i => i.Price);
            result.ShouldHaveValidationErrorFor(i => i.StockQuantity);
        }
    }
}