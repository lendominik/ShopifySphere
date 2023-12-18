using MediatR;
using Shop.Application.ApplicationUser;
using Shop.Application.Exceptions;
using Shop.Domain.Interfaces;

namespace Shop.Application.Category.Commands.DeleteCategory
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserContext _userContext;

        public DeleteCategoryCommandHandler(IUserContext userContext, ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _userContext = userContext;
        }

        public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var user = _userContext.GetCurrentUser();
            var isEdibable = user != null && (user.IsInRole("Owner"));

            if (!isEdibable)
            {
                return Unit.Value;
            }

            var category = await _categoryRepository.GetByEncodedName(request.EncodedName);

            if (category == null)
            {
                throw new NotFoundException("Category not found.");
            }
            
            await _categoryRepository.Delete(category);

            return Unit.Value;
        }
    }
}
