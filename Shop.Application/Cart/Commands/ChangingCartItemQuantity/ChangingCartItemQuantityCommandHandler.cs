using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shop.Application.Exceptions;
using Shop.Domain.Entities;
using Shop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Cart.Commands.ChangingCartItemQuantity
{
    public class ChangingCartItemQuantityCommandHandler : IRequestHandler<ChangingCartItemQuantityCommand>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ChangingCartItemQuantityCommandHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<Unit> Handle(ChangingCartItemQuantityCommand request, CancellationToken cancellationToken)
        {
            if (_httpContextAccessor == null)
            {
                throw new ArgumentNullException(nameof(_httpContextAccessor));
            }

            var session = _httpContextAccessor.HttpContext.Session;
            var cartId = session.GetString("CartSessionKey");

            var cart = session.GetString("Cart");

            var items = JsonConvert.DeserializeObject<List<CartItem>>(cart);

            var item = items.FirstOrDefault(i => i.Id == request.Id);

            decimal unitPrice = item.UnitPrice / item.Quantity;

            item.Quantity = request.Quantity;

            item.UnitPrice = unitPrice * request.Quantity;

            var serializedCartItems = JsonConvert.SerializeObject(items);
            session.SetString("Cart", serializedCartItems);

            return Unit.Value;
        }
        
    }
}
