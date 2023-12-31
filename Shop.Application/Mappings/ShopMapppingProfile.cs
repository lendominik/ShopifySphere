using AutoMapper;
using Shop.Application.Category;
using Shop.Application.Category.Commands.CreateCategory;
using Shop.Application.Category.Commands.EditCategory;
using Shop.Application.Item;
using Shop.Application.Item.Commands.CreateItem;
using Shop.Application.Item.Commands.EditItem;
using Shop.Application.Order;
using Shop.Application.Order.Commands.CreateOrder;
using Shop.Domain.Entities;

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

            CreateMap<CreateOrderCommand, Domain.Entities.Order>()
            .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(_ => OrderStatus.Pending))
            .ForMember(dest => dest.IsPaid, opt => opt.MapFrom(_ => false));
        }
    }
}
