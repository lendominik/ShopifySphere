using AutoMapper;
using MediatR;
using Shop.Application.ApplicationUser;
using Shop.Application.Services;
using Shop.Domain.Interfaces;

namespace Shop.Application.Category.Commands.EditCategory
{
    public class EditCategoryCommandHandler : IRequestHandler<EditCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IAccessControlService _accessControlService;

        public EditCategoryCommandHandler(ICategoryRepository categoryRepository, IAccessControlService accessControlService, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _accessControlService = accessControlService;
        }

        public async Task<Unit> Handle(EditCategoryCommand request, CancellationToken cancellationToken)
        {
            if (!_accessControlService.IsEditable())
            {
                return Unit.Value;
            }

            var category = await _categoryRepository.GetByEncodedName(request.EncodedName);

            category.Description = request.Description;

            await _categoryRepository.Commit();

            return Unit.Value;
        }
    }
}
