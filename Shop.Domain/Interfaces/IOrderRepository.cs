using Shop.Domain.Entities;

namespace Shop.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task Create(Domain.Entities.Order order);
        Task<List<Order>> GetUserOrders(string email);
        Task<List<Order>> GetAllOrders();
        Task<Order> GetOrderById(int orderId);
        Task CancelOrder(int orderId);
        Task CompleteOrder(int orderId);
        Task ShipOrder(int orderId);
        Task SetOrderPaidStauts(Order order);
        Task Commit();
    }
}
