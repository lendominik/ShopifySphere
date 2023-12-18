using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;
using Shop.Domain.Interfaces;
using Shop.Infrastructure.Persistence;

namespace Shop.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ShopDbContext _dbContext;

        public OrderRepository(ShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Commit()
        {
            await _dbContext.SaveChangesAsync();
        }
        public async Task CancelOrder(Order order)
        {
            order.OrderStatus = OrderStatus.Cancelled;
            await Commit();
        }

        public async Task CompleteOrder(Order order)
        {
            order.OrderStatus = OrderStatus.Delivered;
            await Commit();
        }
        public async Task Create(Order order)
        {
            _dbContext.Orders.Add(order);
            await Commit();
        }
        public async Task SetOrderPaidStauts(Order order)
        {
            order.IsPaid = true;
            await Commit();
        }
        public async Task ShipOrder(Order order)
        {
            order.OrderStatus = OrderStatus.Shipped;
            await Commit();
        }
        public async Task<List<Order>> GetAllOrders()
            => await _dbContext.Orders
            .Select(u => new Order { Id = u.Id, Email = u.Email, FirstName = u.FirstName, LastName = u.LastName, IsPaid = u.IsPaid, OrderStatus = u.OrderStatus, OrderDate = u.OrderDate })
            .ToListAsync();
        public async Task<Order> GetOrderById(int orderId)
            => await _dbContext.Orders.Include(order => order.CartItems)
            .ThenInclude(cartItem => cartItem.Item)
            .Where(o => o.Id == orderId)
            .FirstOrDefaultAsync();
        public async Task<List<Order>> GetUserOrders(string email)
            => await _dbContext.Orders
            .Select(u => new Order { Id = u.Id, Email = u.Email, IsPaid = u.IsPaid, OrderStatus = u.OrderStatus, OrderDate = u.OrderDate })
            .Where(o => o.Email == email)
            .ToListAsync();
    }
}
