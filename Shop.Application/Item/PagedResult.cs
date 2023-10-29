﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Item
{
    public class PagedResult
    {
        public int TotalItemsCount { get; set; }
        public int ItemsFrom { get; set; }
        public int ItemsTo { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public IEnumerable<Domain.Entities.Category> Categories { get; set; }
        public IEnumerable<ItemDto> Items { get; set; }

        public PagedResult(IEnumerable<ItemDto> items, int totalCount, int pageSize, int pageNumber, IEnumerable<Domain.Entities.Category> categories)
        {
            Items = items;
            PageSize = pageSize;
            TotalItemsCount = totalCount;
            ItemsFrom = pageSize * (pageNumber - 1) + 1;
            ItemsTo = ItemsFrom + pageSize - 1;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            Categories = categories;
        }
    }
}
