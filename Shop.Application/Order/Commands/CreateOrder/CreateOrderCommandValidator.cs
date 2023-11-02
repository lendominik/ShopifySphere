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
                .NotEmpty();

            RuleFor(o => o.FirstName) 
                .NotEmpty();

            RuleFor(o => o.LastName)
                .NotEmpty();

            RuleFor(o => o.PhoneNumber)
                .MinimumLength(8)
                .MaximumLength(12)
                .NotEmpty();
        }
    }
}
