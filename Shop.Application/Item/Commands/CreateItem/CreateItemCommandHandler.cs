using AutoMapper;
using MediatR;
using Shop.Application.Exceptions;
using Shop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Item.Commands.CreateItem
{
    public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand>
    {
        private readonly IItemRepository _itemRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CreateItemCommandHandler(IItemRepository itemRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _itemRepository = itemRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {
            var item = _mapper.Map<Domain.Entities.Item>(request);

            var category = await _categoryRepository.GetByEncodedName(request.CategoryEncodedName!);

            if(category == null)
            {
                throw new NotFoundException("Podana kategoria nie istnieje.");
            }

            item.EncodeName();
            item.Category = category;

            await _itemRepository.Create(item);

            return Unit.Value;
        }
    }
}
