using AutoMapper;
using MediatR;
using Shop.Domain.Interfaces;
using System.Linq.Expressions;
using Shop.Application.Exceptions;

namespace Shop.Application.Item.Queries.GetAllItems
{
    public class GetAllItemsQueryHandler : IRequestHandler<GetAllItemsQuery, PagedResult>
    {
        private readonly IItemRepository _itemRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public GetAllItemsQueryHandler(IItemRepository itemRepository, ICategoryRepository categoryRepository,IMapper mapper)
        {
            _itemRepository = itemRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult> Handle(GetAllItemsQuery request, CancellationToken cancellationToken)
        {
            request.PageNumber = request.PageNumber < 1 ? 1 : request.PageNumber;
            request.PageSize = request.PageSize < 1 ? 10 : request.PageSize;

            var items = await _itemRepository.GetAll();

            if(items == null)
            {
                throw new NotFoundException("Items not found.");
            }

            if (!string.IsNullOrEmpty(request.SelectedCategory))
            {
                items = items.Where(c => c.Category.Name == request.SelectedCategory);
            }

            if (!string.IsNullOrEmpty(request.SearchPhrase))
            {
                items = items.Where(r => r.Name.ToLower().Contains(request.SearchPhrase.ToLower()) || r.Description.ToLower().Contains(request.SearchPhrase.ToLower()));
            }  

            if (!string.IsNullOrEmpty(request.SortBy))
            {
                var columnsSelectors = new Dictionary<string, Expression<Func<Domain.Entities.Item, object>>>
            {
                {nameof(Domain.Entities.Item.Name), x => x.Name},
                {nameof(Domain.Entities.Item.Category), x => x.Category.Name},
                {nameof(Domain.Entities.Item.Price), x => x.Price},
            };

                if (columnsSelectors.TryGetValue(request.SortBy, out var selectedColumn))
                {
                    items = request.SortDirection == "ASC" ? items.AsQueryable().OrderBy(selectedColumn) : items.AsQueryable().OrderByDescending(selectedColumn);
                }
            }

            var itemsCount = items.Count();
            var itemsToDisplay = items.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToList();

            var itemDtos = _mapper.Map<IEnumerable<ItemDto>>(itemsToDisplay);

            var categories = await _categoryRepository.GetAll();

            var result = new PagedResult(itemDtos, itemsCount, request.PageSize,request.PageNumber, categories);

            return result;
        }
    }
}
