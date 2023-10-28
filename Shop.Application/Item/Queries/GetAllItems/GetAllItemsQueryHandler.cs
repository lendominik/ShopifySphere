using AutoMapper;
using MediatR;
using Shop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Linq.Expressions;

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

            items = items
                         .Where(r => request.SearchPhrase == null || (r.Name.ToLower().Contains(request.SearchPhrase.ToLower()) ||
                         (r.Description.ToLower().Contains(request.SearchPhrase.ToLower()))));

            if (!string.IsNullOrEmpty(request.SortBy))
            {
                var columnsSelectors = new Dictionary<string, Expression<Func<Domain.Entities.Item, object>>>
            {
                {nameof(Domain.Entities.Item.Name), x => x.Name},
                {nameof(Domain.Entities.Item.Category), x => x.Category.Name},
                {nameof(Domain.Entities.Item.Description), x => x.Description},
            };

                var selectedColumn = columnsSelectors[request.SortBy];

                items = request.SortDirection == "ASC"
                    ? items.AsQueryable().OrderBy(selectedColumn)
                    : items.AsQueryable().OrderByDescending(selectedColumn);
            }

            var itemsCount = items.Count();

            items = items
                .Skip(request.PageSize * (request.PageNumber - 1))
                .Take(request.PageSize).ToList();

            var itemDtos = _mapper.Map<IEnumerable<ItemDto>>(items);

            var result = new PagedResult(itemDtos, itemsCount, request.PageSize,request.PageNumber);

            return result;
        }
    }
}
