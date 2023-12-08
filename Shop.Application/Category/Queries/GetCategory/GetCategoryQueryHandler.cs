using AutoMapper;
using MediatR;
using Shop.Application.Exceptions;
using Shop.Domain.Interfaces;

namespace Shop.Application.Category.Queries.GetCategory
{
    public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, CategoryDto>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public GetCategoryQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<CategoryDto> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByEncodedName(request.EncodedName);

            if(category == null)
            {
                throw new NotFoundException("Category not found.");
            }

            var categoryDto = _mapper.Map<CategoryDto>(category);

            return categoryDto;
        }
    }
}
