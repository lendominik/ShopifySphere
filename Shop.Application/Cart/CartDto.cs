using Shop.Domain.Entities;

namespace Shop.Application.Cart
{
    public class CartDto
    {
        public string Id { get; set; }
        public decimal CartTotal { get; set; } // suma zamówienia
        public List<CartItem> CartItems { get; set; } 
    }
}
