using AutoMapper;
using MediatR;
using Shop.Application.ApplicationUser;
using Shop.Application.Exceptions;
using Shop.Application.Services;
using Shop.Domain.Interfaces;

namespace Shop.Application.Category.Commands.EditCategory
{
    public class EditCategoryCommandHandler : IRequestHandler<EditCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IAccessControlService _accessControlService;
        private readonly IUserContext _userContext;

        public EditCategoryCommandHandler(ICategoryRepository categoryRepository, IAccessControlService accessControlService, IMapper mapper, IUserContext userContext)
        {
            _categoryRepository = categoryRepository;
            _accessControlService = accessControlService;
            _userContext = userContext;
        }

        public async Task<Unit> Handle(EditCategoryCommand request, CancellationToken cancellationToken)
        {
            if (!_accessControlService.IsEditable(_userContext))
            {
                return Unit.Value;
            }

            var category = await _categoryRepository.GetByEncodedName(request.EncodedName);

            if ( category == null)
            {
                throw new NotFoundException("Category not found.");
            }

            category.Description = request.Description;

            await _categoryRepository.Commit();

            return Unit.Value;
        }
    }
}
