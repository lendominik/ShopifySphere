using MediatR;

namespace Shop.Application.Cart.Commands.RemoveFromCart
{
    public class RemoveFromCartCommand : IRequest
    {
        public Guid Id { get; set; } // id cardItem
    }
}
