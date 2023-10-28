using AutoMapper;
using MediatR;
using Shop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Shop.Application.Item.Queries.GetAllItems
{
    public class GetAllItemsQueryHandler : IRequestHandler<GetAllItemsQuery, PagedResult>
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;

        public GetAllItemsQueryHandler(IItemRepository itemRepository, IMapper mapper)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult> Handle(GetAllItemsQuery request, CancellationToken cancellationToken)
        {
            var items = await _itemRepository.GetAll();

            var itemsCount = items.Count();

            items = items
                         //.Where(r => request.SearchPhrase == null || (r.Name.ToLower().Contains(request.SearchPhrase.ToLower())))
                         .Skip(request.PageSize * (request.PageNumber - 1))
                         .Take(request.PageSize).ToList();

            var itemDtos = _mapper.Map<IEnumerable<ItemDto>>(items);

            var result = new PagedResult(itemDtos, itemsCount, request.PageSize,request.PageNumber);

            return result;
        }
    }
}
