using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Item.Commands.EditItem
{
    public class EditItemCommandValidator : AbstractValidator<EditItemCommand>
    {
        public EditItemCommandValidator()
        {
            RuleFor(c => c.Price)
                .NotEmpty();

            RuleFor(c => c.ProductImage) 
                .NotEmpty();

            RuleFor(c => c.StockQuantity) 
                .NotEmpty();

            RuleFor(c => c.Description)
                .NotEmpty()
                .MinimumLength(8);
        }
    }
}
