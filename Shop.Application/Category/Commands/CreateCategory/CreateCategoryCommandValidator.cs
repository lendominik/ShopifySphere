using FluentValidation;
using Shop.Domain.Interfaces;

namespace Shop.Application.Category.Commands.CreateCategory
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator(ICategoryRepository repository)
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .MinimumLength(3)
                .Custom((value, context) =>
                {
                    if (repository.CategoryExists(value))
                    {
                        context.AddFailure("Kategoria o tej nazwie istnieje już w bazie danych.");
                    }
                });

            RuleFor(c => c.Description)
                .NotEmpty()
                .MinimumLength(8);
        }
    }
}
