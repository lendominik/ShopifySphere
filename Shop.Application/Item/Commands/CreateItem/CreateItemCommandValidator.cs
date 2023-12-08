using FluentValidation;
using Shop.Domain.Interfaces;

namespace Shop.Application.Item.Commands.CreateItem
{
    public class CreateItemCommandValidator : AbstractValidator<CreateItemCommand>
    {
        public CreateItemCommandValidator(IItemRepository repository)
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .MinimumLength(3)
                .Custom((value, context) =>
                {
                    if (repository.ItemExists(value))
                    {
                        context.AddFailure("The category with this name already exists in the database.");
                    }
                });

            RuleFor(c => c.Description)
                .NotEmpty()
                .MinimumLength(8)
                .NotNull();

            RuleFor(c => c.Price)
                .NotEmpty();

            RuleFor(c => c.StockQuantity)
                .NotEmpty();

            RuleFor(c => c.CategoryEncodedName)
                .NotEmpty().NotNull();

        }
    }
}
