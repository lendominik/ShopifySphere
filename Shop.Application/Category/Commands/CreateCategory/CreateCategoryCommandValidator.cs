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
                        context.AddFailure("The category with this name already exists in the database.");
                    }
                });

            RuleFor(c => c.Description)
                .NotEmpty()
                .MinimumLength(8);
        }
    }
}
