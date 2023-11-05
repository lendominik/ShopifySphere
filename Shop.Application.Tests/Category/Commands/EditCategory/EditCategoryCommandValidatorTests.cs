using Xunit;
using Shop.Application.Category.Commands.EditCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Shop.Application.Category.Commands.CreateCategory;
using Shop.Domain.Interfaces;
using FluentValidation.TestHelper;

namespace Shop.Application.Category.Commands.EditCategory.Tests
{
    public class EditCategoryCommandValidatorTests
    {
        [Fact()]
        public void Validate_WithValidCommand_ShouldNotHaveValidationError()
        {
            var validator = new EditCategoryCommandValidator();

            var command = new EditCategoryCommand()
            {
                Description = "description",
                EncodedName = "categoryname",
            };

            var result = validator.TestValidate(command);

            result.ShouldNotHaveAnyValidationErrors();
        }
        [Fact()]
        public void Validate_WithValidCommand_ShouldHaveValidationError()
        {
            var validator = new EditCategoryCommandValidator();

            var command = new EditCategoryCommand()
            {
                Description = "desc",
                EncodedName = "categoryname",
            };

            var result = validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(i => i.Description);
        }
    }
}