using Shop.Domain.Entities;

namespace Shop.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task Create(Order order);
        Task<List<Order>> GetUserOrders(string email);
        Task<List<Order>> GetAllOrders();
        Task<Order> GetOrderById(int orderId);
        Task CancelOrder(Order order);
        Task CompleteOrder(Order order);
        Task ShipOrder(Order order);
        Task SetOrderPaidStauts(Order order);
        Task Commit();
    }
}
