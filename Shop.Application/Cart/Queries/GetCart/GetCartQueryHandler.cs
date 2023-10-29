using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
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
            var cart = await _cartRepository.GetCart(cartId);

            var cartItems = await _cartRepository.GetCartItems(cartId);

            var cartDto = _mapper.Map<CartDto>(cart);

            return cartDto;
        }
    }
}
