using Shop.Domain.Entities;

namespace Shop.Application.Order.Commands.CreateOrder
{
    public static class CalculateCartTotal
    {
        public static decimal Calculate(List<CartItem> cartItems)
        {
            if (cartItems == null)
            {
                return 0;
            }

            decimal total = cartItems.Sum(item => item.UnitPrice);

            return total;
        }
    }
}
