using AutoMapper;
using MediatR;
using Shop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Category.Commands.CreateCategory
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<Domain.Entities.Category>(request);

            category.EncodeName();
            
            await _categoryRepository.Create(category);

            return Unit.Value;
        }
    }
}
