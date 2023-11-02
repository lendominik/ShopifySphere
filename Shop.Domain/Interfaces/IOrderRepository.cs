using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task Create(Domain.Entities.Order order);
        Task<List<Order>> GetUserOrders(string email);
    }
}
