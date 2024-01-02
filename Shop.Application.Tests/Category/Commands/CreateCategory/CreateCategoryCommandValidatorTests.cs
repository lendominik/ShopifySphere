using Xunit;
using Shop.Application.Category.Commands.CreateCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Shop.Application.Item.Commands.CreateItem;
using Shop.Domain.Interfaces;
using FluentValidation.TestHelper;

namespace Shop.Application.Category.Commands.CreateCategory.Tests
{
    public class CreateCategoryCommandValidatorTests
    {
        [Fact()]
        public void Validate_WithValidCommand_ShouldNotHaveValidationError()
        {
            // Assert
            var categoryRepositoryMock = new Mock<ICategoryRepository>();

            var validator = new CreateCategoryCommandValidator(categoryRepositoryMock.Object);

            var command = new CreateCategoryCommand()
            {
                Description = "description",
                EncodedName = "category",
                Name = "name"
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
            var categoryRepositoryMock = new Mock<ICategoryRepository>();

            var validator = new CreateCategoryCommandValidator(categoryRepositoryMock.Object);

            var command = new CreateCategoryCommand()
            {
                Description = "den",
                EncodedName = "category",
            };

            // Act
            var result = validator.TestValidate(command);

            // Arrange
            result.ShouldHaveValidationErrorFor(i => i.Name);
            result.ShouldHaveValidationErrorFor(i => i.Description);
        }

    }
}