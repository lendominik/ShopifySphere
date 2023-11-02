using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Interfaces
{
    public interface IItemRepository
    {
        Task<IEnumerable<Domain.Entities.Item>> GetAll();
        Task Create(Domain.Entities.Item item);
        Task<Domain.Entities.Item> GetByEncodedName(string encodedName);
        Task<Item> GetByName(string name);
        Task Commit();
        Task Delete(Item item);
        Task<bool> DeductStockQuantity(Item item, int quantityToDeduct);
    }
}
