using MediatR;

namespace Shop.Application.Cart.Commands.ChangingCartItemQuantity
{
    public class ChangingCartItemQuantityCommand : IRequest
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
    }
}
