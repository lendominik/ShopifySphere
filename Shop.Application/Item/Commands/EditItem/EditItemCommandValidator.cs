using FluentValidation;

namespace Shop.Application.Item.Commands.EditItem
{
    public class EditItemCommandValidator : AbstractValidator<EditItemCommand>
    {
        public EditItemCommandValidator()
        {
            RuleFor(c => c.Price)
                .NotEmpty();


            RuleFor(c => c.StockQuantity) 
                .NotEmpty();

            RuleFor(c => c.Description)
                .NotEmpty()
                .MinimumLength(8);
        }
    }
}
