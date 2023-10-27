using AutoMapper;
using Shop.Application.Category;
using Shop.Application.Category.Commands.CreateCategory;
using Shop.Application.Category.Commands.EditCategory;
using Shop.Application.Item;
using Shop.Application.Item.Commands.CreateItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        }
    }
}
