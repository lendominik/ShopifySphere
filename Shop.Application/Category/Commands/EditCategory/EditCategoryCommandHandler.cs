using AutoMapper;
using MediatR;
using Shop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Category.Commands.EditCategory
{
    public class EditCategoryCommandHandler : IRequestHandler<EditCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public EditCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(EditCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByEncodedName(request.EncodedName);

            category.Description = request.Description;
            category.Name = request.Name;

            category.EncodeName();

            await _categoryRepository.Commit();

            return Unit.Value;
        }
    }
}
