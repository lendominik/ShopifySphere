using Xunit;
using FluentValidation.TestHelper;

namespace Shop.Application.Order.Commands.CreateOrder.Tests
{
    public class CreateOrderCommandValidatorTests
    {
        [Fact()]
        public async void Validate_WithValidCommand_ShouldNotHaveValidationError()
        {
            // Arrange
            var validator = new CreateOrderCommandValidator();
            var command = new CreateOrderCommand()
            {
                City = "Test",
                Email = "test@test.com",
                FirstName = "Jan",
                LastName = "Kowalski",
                PostalCode = "02-791",
                PhoneNumber = "123-123-123",
                Street = "Testowa",
                Address = "Test"
            };

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
        [Fact()]
        public async void Validate_WithValidCommand_ShouldHaveValidationError()
        {
            // Arrange
            var validator = new CreateOrderCommandValidator();
            var command = new CreateOrderCommand()
            {
                PostalCode = "02791",
                PhoneNumber = "123123123",
                Street = "Testowa",
            };

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.PostalCode);
            result.ShouldHaveValidationErrorFor(c => c.PhoneNumber);
            result.ShouldHaveValidationErrorFor(c => c.FirstName);
            result.ShouldHaveValidationErrorFor(c => c.LastName);
            result.ShouldHaveValidationErrorFor(c => c.Address);
            result.ShouldHaveValidationErrorFor(c => c.City);
        }
    }
}