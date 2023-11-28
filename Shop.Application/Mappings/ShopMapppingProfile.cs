using AutoMapper;
using Shop.Application.Category;
using Shop.Application.Category.Commands.CreateCategory;
using Shop.Application.Category.Commands.EditCategory;
using Shop.Application.Item;
using Shop.Application.Item.Commands.CreateItem;
using Shop.Application.Item.Commands.EditItem;
using Shop.Application.Order;
using Shop.Application.Order.Commands.CreateOrder;

namespace Shop.Application.Mappings
{
    public class ShopMappingProfile : Profile
    {
        public ShopMappingProfile()
        {
            CreateMap<Domain.Entities.Item, ItemDto>()
                .ReverseMap();

            CreateMap<Domain.Entities.Category, CategoryDto>()
                .ReverseMap();

            CreateMap<CreateItemCommand, Domain.Entities.Item>();

            CreateMap<CreateCategoryCommand, Domain.Entities.Category>();

            CreateMap<CategoryDto, EditCategoryCommand>();

            CreateMap<ItemDto, EditItemCommand>();

            CreateMap<Domain.Entities.Order, OrderDto>();

            CreateMap<CreateOrderCommand, Domain.Entities.Order>();
        }
    }
}
