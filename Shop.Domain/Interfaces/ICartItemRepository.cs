using Shop.Domain.Entities;

namespace Shop.Domain.Interfaces
{
    public interface ICartItemRepository
    {
        Task Create(CartItem cartItem);
    }
}
