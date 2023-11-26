using MediatR;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Shop.Application.Exceptions;
using Shop.Domain.Entities;
using Shop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Cart.Commands.RemoveFromCart
{
    public class RemoveFromCartCommandHandler : IRequestHandler<RemoveFromCartCommand>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RemoveFromCartCommandHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Unit> Handle(RemoveFromCartCommand request, CancellationToken cancellationToken)
        {
            if (_httpContextAccessor == null)
            {
                throw new ArgumentNullException(nameof(_httpContextAccessor));
            }

            var session = _httpContextAccessor.HttpContext.Session;
            var cartId = session.GetString("CartSessionKey");

            if (string.IsNullOrWhiteSpace(cartId))
            {
                cartId = Guid.NewGuid().ToString();
                session.SetString("CartSessionKey", cartId);
            }

            var cart = session.GetString("Cart");
            var items = JsonConvert.DeserializeObject<List<CartItem>>(cart);

            var cartItem = items.FirstOrDefault(i => i.Id == request.Id);

            if (cartId == null || cartItem == null)
            {
                throw new NotFoundException("Nie znaleziono kosza użytkownika lub podanego przedmiotu.");
            }

            items.Remove(cartItem);

            var serializedCartItems = JsonConvert.SerializeObject(items);
            session.SetString("Cart", serializedCartItems);

            return Unit.Value;
        }
    }
}
