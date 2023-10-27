﻿using AutoMapper;
using Shop.Application.Item;
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
        }
    }
}
