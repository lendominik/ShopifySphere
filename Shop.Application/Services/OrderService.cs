using Shop.Application.Exceptions;
using Shop.Domain.Entities;

namespace Shop.Application.Services
{
    public interface IOrderService
    {
        decimal Calculate(List<CartItem> cartItems);
        void CheckStockQuantity(IEnumerable<CartItem> cartItems);
    }

    public class OrderService : IOrderService
    {
        public decimal Calculate(List<CartItem> cartItems)
        {
            if (cartItems == null)
            {
                return 0;
            }

            decimal total = cartItems.Sum(item => item.UnitPrice);

            return total;
        }
        public void CheckStockQuantity(IEnumerable<CartItem> cartItems)
        {
            foreach (var item in cartItems)
            {
                if (item.Item.StockQuantity < item.Quantity || item.Item.StockQuantity < 0 || item.Quantity <= 0)
                {
                    throw new OutOfStockException($"There are not enough items in stock for {item.Item.Name}.");
                }

                item.Item.StockQuantity -= item.Quantity;
            }
        }
    }
}
