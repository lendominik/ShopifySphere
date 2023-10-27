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
    }
}
