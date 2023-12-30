using AutoMapper;
using MediatR;
using Shop.Domain.Interfaces;
using Shop.Application.Exceptions;
using Shop.Application.Common.PagedResult;
using Shop.Application.Services;
using Shop.Application.Services.ItemServices;

namespace Shop.Application.Item.Queries.GetAllItems
{
    public class GetAllItemsQueryHandler : IRequestHandler<GetAllItemsQuery, ItemPagedResult<ItemDto>>
    {
        private readonly IItemRepository _itemRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IItemFilterService _itemFilterService;
        private readonly IItemSortService _itemSortService;
        private readonly IPaginationService _paginationService;

        public GetAllItemsQueryHandler(IPaginationService paginationService, IItemSortService itemSortService, IItemFilterService itemFilterService, IItemRepository itemRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _itemRepository = itemRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _itemFilterService = itemFilterService;
            _itemSortService = itemSortService;
            _paginationService = paginationService;
        }

        public async Task<ItemPagedResult<ItemDto>> Handle(GetAllItemsQuery request, CancellationToken cancellationToken)
        {
            var items = _itemRepository.GetAll();

            if(items == null)
            {
                throw new NotFoundException("Items not found.");
            }

            items = _itemFilterService.FilterByCategory(items, request.SelectedCategory);

            items = _itemFilterService.FilterBySearchPhrase(items, request.SearchPhrase);

            items = _itemSortService.SortItems(items, request.SortBy, request.SortDirection);

            var itemsCount = items.Count();

            var itemsToDisplay = _paginationService.PaginationSkipAndTake(items, request.PageNumber, request.PageSize);

            var itemDtos = _mapper.Map<IEnumerable<ItemDto>>(itemsToDisplay);

            var categories = await _categoryRepository.GetAll();

            var result = new ItemPagedResult<ItemDto>(itemDtos, itemsCount, request.PageSize, request.PageNumber, categories);

            return result;
        }
    }
}
