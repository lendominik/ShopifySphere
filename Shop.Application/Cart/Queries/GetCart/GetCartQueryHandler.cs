using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Shop.Application.Services;
using Shop.Domain.Entities;
using System.Text;

namespace Shop.Application.Cart.Queries.GetCart
{
    public class GetCartQueryHandler : IRequestHandler<GetCartQuery, CartDto>
    {
        private readonly ICartService _cartService;

        public GetCartQueryHandler(ICartService cartService)
        {
            _cartService = cartService;
        }
        public async Task<CartDto> Handle(GetCartQuery request, CancellationToken cancellationToken)
        {
            var cartId = _cartService.GetOrCreateCartId();

            var items = _cartService.GetCartItems();
  
            var cartDto = new CartDto
            {
                CartItems = items,
                CartTotal = _cartService.CalculateCartTotal(items),
                Id = cartId
            };
           
            return cartDto;
        }
    }
}
