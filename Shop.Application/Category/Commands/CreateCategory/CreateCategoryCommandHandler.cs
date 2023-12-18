using AutoMapper;
using MediatR;
using Shop.Application.ApplicationUser;
using Shop.Application.Services;
using Shop.Domain.Interfaces;

namespace Shop.Application.Category.Commands.CreateCategory
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IAccessControlService _accessControlService;

        public CreateCategoryCommandHandler(IAccessControlService accessControlService, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _accessControlService = accessControlService;
        }
        public async Task<Unit> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            if (!_accessControlService.IsEditable())
            {
                return Unit.Value;
            }

            var category = _mapper.Map<Domain.Entities.Category>(request);

            category.EncodeName();
            
            await _categoryRepository.Create(category);

            return Unit.Value;
        }
    }
}
