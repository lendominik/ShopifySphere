using AutoMapper;
using MediatR;
using Shop.Application.ApplicationUser;
using Shop.Domain.Interfaces;

namespace Shop.Application.Category.Commands.CreateCategory
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;

        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper, IUserContext userContext)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _userContext = userContext;
        }
        public async Task<Unit> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var user = _userContext.GetCurrentUser();
            var isEdibable = user != null && (user.IsInRole("Owner"));

            if (!isEdibable)
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
