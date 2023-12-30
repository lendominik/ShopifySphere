using Microsoft.AspNetCore.Http;

namespace Shop.Application.Services.CartServices
{
    public interface ICartIdProviderService
    {
        string GetOrCreateCartId(IHttpContextAccessor httpContextAccessor);
    }
    public class CartIdProviderService : ICartIdProviderService
    {
        public string GetOrCreateCartId(IHttpContextAccessor httpContextAccessor)
        {
            var session = httpContextAccessor.HttpContext.Session;

            var cartId = session.GetString("CartSessionKey");

            if (string.IsNullOrWhiteSpace(cartId))
            {
                cartId = Guid.NewGuid().ToString();
                session.SetString("CartSessionKey", cartId);
            }

            return cartId;
        }
    }
}
