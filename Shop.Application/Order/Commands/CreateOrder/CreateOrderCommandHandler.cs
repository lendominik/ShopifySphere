﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Shop.Application.ApplicationUser;
using Shop.Application.Exceptions;
using Shop.Domain.Entities;
using Shop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Order.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand>
    {
        
        private readonly IOrderRepository _orderRepository;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public CreateOrderCommandHandler(IOrderRepository orderRepository, IItemRepository itemRepository ,IHttpContextAccessor httpContextAccessor,ICartItemRepository cartItemRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _cartItemRepository = cartItemRepository;
            _itemRepository = itemRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var cartId = await _cartItemRepository.GetCartId(_httpContextAccessor);
            var cartItems = await _cartItemRepository.GetCartItems(cartId);

            if (cartItems == null)
            {
                throw new NotFoundException("Koszyk jest pusty.");
            }

            var order = _mapper.Map<Domain.Entities.Order>(request);
 
            order.CartItems = cartItems;
            order.CartTotal = await _cartItemRepository.CalculateCartTotal(cartItems);
            order.OrderDate = DateTime.Now;

            foreach( var cartItem in cartItems)
            {
                var item = await _itemRepository.GetByEncodedName(cartItem.Item.EncodedName);
                await _itemRepository.DeductStockQuantity(item, cartItem.Quantity);
            }

            //tu musi być sprawdzenie czy produkt został opłacony!

            await _orderRepository.Create(order);
            await _cartItemRepository.RemoveCartItemsByCartId(cartId);

            return Unit.Value;
        }
    }
}
