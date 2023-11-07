using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            .NotEmpty().WithMessage("Kod pocztowy nie może być pusty.")
            .Matches(@"^\d{2}-\d{3}$").WithMessage("Niepoprawny format kodu pocztowego (xx-xxx).");

            RuleFor(o => o.FirstName) 
                .NotEmpty();

            RuleFor(o => o.LastName)
                .NotEmpty();

            RuleFor(o => o.PhoneNumber)
                .NotEmpty().WithMessage("Numer telefonu nie może być pusty.")
                .Matches(@"^\d{3}-\d{3}-\d{3}$").WithMessage("Niepoprawny format numeru telefonu (123-456-789).");
        }
    }
}
