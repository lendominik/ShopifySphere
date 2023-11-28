using MediatR;

namespace Shop.Application.Cart.Commands.ChangingCartItemQuantity
{
    public class ChangingCartItemQuantityCommand : IRequest
    {
        public Guid Id { get; set; } // id cartItem
        public int Quantity { get; set; } // new quantity
    }
}
