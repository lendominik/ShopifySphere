using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Domain.Entities.Category>> GetAll();
        Task Create(Domain.Entities.Category category);
        Task<Domain.Entities.Category> GetByEncodedName(string encodedName);
        Task Commit();
    }
}
