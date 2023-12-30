using Shop.Domain.Entities;

namespace Shop.Application.Services.CartServices
{
    public interface ICartCalculatorService
    {
        decimal CalculateCartTotal(List<CartItem> cartItems);
        void UpdateCartItemPriceAndQuantity(CartItem item, int newQuantity);
    }
    public class CartCalculatorService : ICartCalculatorService
    {
        public decimal CalculateCartTotal(List<CartItem> cartItems)
        {
            decimal total = 0;
            foreach (var cartItem in cartItems)
            {
                total += cartItem.UnitPrice;
            }
            return total;
        }
        public void UpdateCartItemPriceAndQuantity(CartItem item, int newQuantity)
        {
            decimal unitPricePerItem = item.UnitPrice / item.Quantity;
            item.Quantity = newQuantity;
            item.UnitPrice = unitPricePerItem * newQuantity;
        }
    }
}
