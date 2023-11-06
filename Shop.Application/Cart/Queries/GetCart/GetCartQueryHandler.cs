using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Shop.Domain.Entities;
using Shop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Cart.Queries.GetCart
{
    public class GetCartQueryHandler : IRequestHandler<GetCartQuery, CartDto>
    {
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public GetCartQueryHandler(ICartItemRepository cartItemRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _cartItemRepository = cartItemRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }
        public async Task<CartDto> Handle(GetCartQuery request, CancellationToken cancellationToken)
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

            var cartItems = await _cartItemRepository.GetCartItems(cartId);

            var cartDto = new CartDto
            {
                CartItems = cartItems,
                CartTotal = CalculateCartTotal(cartItems),
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
