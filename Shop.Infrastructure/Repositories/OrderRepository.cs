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
        public async Task CancelOrder(int orderId)
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
            order.OrderStatus = OrderStatus.Cancelled;
            await _dbContext.SaveChangesAsync();
        }

        public async Task CompleteOrder(int orderId)
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
            order.OrderStatus = OrderStatus.Delivered;
            await _dbContext.SaveChangesAsync();
        }
        public async Task Create(Order order)
        {
            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();
        }
        public async Task SetOrderPaidStauts(Order order)
        {
            order.IsPaid = true;
            await Commit();
            await _dbContext.SaveChangesAsync();
        }
        public async Task ShipOrder(int orderId)
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
            order.OrderStatus = OrderStatus.Shipped;
            await _dbContext.SaveChangesAsync();
        }
        public async Task Commit()
        {
            await _dbContext.SaveChangesAsync();
        }
        public async Task<List<Order>> GetAllOrders()
            => await _dbContext.Orders.ToListAsync();
        public async Task<Order> GetOrderById(int orderId)
            => await _dbContext.Orders.Include(order => order.CartItems)
                .ThenInclude(cartItem => cartItem.Item)
                .Where(o => o.Id == orderId)
                .FirstOrDefaultAsync();
        public async Task<List<Order>> GetUserOrders(string email)
            => await _dbContext.Orders
                .Select(u => new Order { Id = u.Id, Email = u.Email, IsPaid = u.IsPaid, OrderStatus = u.OrderStatus })
                .Where(o => o.Email == email).ToListAsync();
    }
}
