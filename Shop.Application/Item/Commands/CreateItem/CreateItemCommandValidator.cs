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
                    var existingItem = repository.GetByName(value).Result;
                    if (existingItem != null)
                    {
                        context.AddFailure("Przedmiot o tej nazwie istnieje już w bazie danych.");
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
