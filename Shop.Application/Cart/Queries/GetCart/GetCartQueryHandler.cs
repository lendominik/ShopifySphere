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
        private readonly ICartRepository _cartRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public GetCartQueryHandler(ICartRepository cartRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }
        public async Task<CartDto> Handle(GetCartQuery request, CancellationToken cancellationToken)
        {
            var cartId = await _cartRepository.GetCartId(_httpContextAccessor);

            await Console.Out.WriteLineAsync(cartId);

            var cartItems = await _cartRepository.GetCartItems(cartId);

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
