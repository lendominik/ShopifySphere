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
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly ICartService _cartService;

        public GetCartQueryHandler(IHttpContextAccessor httpContextAccessor, IMapper mapper, ICartService cartService)
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _cartService = cartService;
        }
        public async Task<CartDto> Handle(GetCartQuery request, CancellationToken cancellationToken)
        {
            var cartId = _cartService.GetOrCreateCartId();
            var cart = _cartService.GetCart();

            var items = _cartService.GetCartItems();
  
            var cartDto = new CartDto
            {
                CartItems = items,
                CartTotal = CalculateCartTotal(items),
                Id = cartId
            };
           
            return cartDto;
        }
        private decimal CalculateCartTotal(List<CartItem> cartItems)
        {
            decimal total = 0;
            foreach (var cartItem in cartItems)
            {
                total += cartItem.UnitPrice;
            }
            return total;
        }
    }
}
