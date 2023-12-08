using FluentValidation;

namespace Shop.Application.Order.Commands.CreateOrder
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(o => o.Street)
                .NotEmpty();

            RuleFor(o => o.City)
                .NotEmpty();

            RuleFor(o => o.Address)
                .NotEmpty();

            RuleFor(o => o.PostalCode)
            .NotEmpty().WithMessage("The postal code cannot be empty.")
            .Matches(@"^\d{2}-\d{3}$").WithMessage("Invalid postal code format (xx-xxx).");

            RuleFor(o => o.FirstName)
                .NotEmpty();

            RuleFor(o => o.LastName)
                .NotEmpty();

            RuleFor(o => o.PhoneNumber)
                .NotEmpty().WithMessage("The phone number cannot be empty.")
                .Matches(@"^\d{3}-\d{3}-\d{3}$").WithMessage("Invalid phone number format (123-456-789).");
        }
    }
}
