using AutoMapper;
using MediatR;
using Shop.Application.ApplicationUser;
using Shop.Domain.Interfaces;

namespace Shop.Application.Category.Commands.EditCategory
{
    public class EditCategoryCommandHandler : IRequestHandler<EditCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;

        public EditCategoryCommandHandler(ICategoryRepository categoryRepository, IUserContext userContext, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _userContext = userContext;
        }

        public async Task<Unit> Handle(EditCategoryCommand request, CancellationToken cancellationToken)
        {
            var user = _userContext.GetCurrentUser();
            var isEdibable = user != null && (user.IsInRole("Owner"));

            if (!isEdibable)
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
