using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAll();
        Task Create(Category category);
        Task Delete(Category category);
        Task<Category> GetByEncodedName(string encodedName);
        Task<Category> GetByName(string name);
        Task Commit();
    }
}
