using Xunit;
using Shop.Application.Item.Commands.CreateItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Shop.Domain.Interfaces;
using FluentValidation.TestHelper;

namespace Shop.Application.Item.Commands.CreateItem.Tests
{
    public class CreateItemCommandValidatorTests
    {
        [Fact()]
        public void Validate_WithValidCommand_ShouldNotHaveValidationError()
        {
            var itemRepositoryMock = new Mock<IItemRepository>();

            var validator = new CreateItemCommandValidator(itemRepositoryMock.Object);

            var command = new CreateItemCommand()
            {
                Name = "name",
                StockQuantity = 1,
                CategoryEncodedName = "category",
                Description = "description",
                Price = 1
            };

            var result = validator.TestValidate(command);

            result.ShouldNotHaveAnyValidationErrors();
        }
        [Fact()]
        public void Validate_WithValidCommand_ShouldHaveValidationError()
        {
            var itemRepositoryMock = new Mock<IItemRepository>();

            var validator = new CreateItemCommandValidator(itemRepositoryMock.Object);

            var command = new CreateItemCommand()
            {
                CategoryEncodedName = "category",
                Description = "de",
                Price = 1
            };

            var result = validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(i => i.Name);
            result.ShouldHaveValidationErrorFor(i => i.Description);
            result.ShouldHaveValidationErrorFor(i => i.StockQuantity);
        }
    }
}