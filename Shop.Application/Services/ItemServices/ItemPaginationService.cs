using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Services.ItemServices
{
    public interface IItemPaginationService
    {
        IEnumerable<Domain.Entities.Item> PaginationSkipAndTake(IQueryable<Domain.Entities.Item> items, int pageNumber, int pageSize);
    }

    public class ItemPaginationService : IItemPaginationService
    {
        public IEnumerable<Domain.Entities.Item> PaginationSkipAndTake(IQueryable<Domain.Entities.Item> items, int pageNumber, int pageSize)
        {
            var itemsToDisplay = items.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return itemsToDisplay;
        }
    }
}
