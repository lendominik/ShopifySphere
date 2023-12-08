using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Shop.Application.Exceptions;
using Shop.Domain.Entities;
using Shop.Domain.Interfaces;
using System.Security.Claims;
using System.Text;

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
            if (_httpContextAccessor == null)
            {
                throw new ArgumentNullException(nameof(_httpContextAccessor));
            }

            var session = _httpContextAccessor.HttpContext.Session;
            var cartId = session.GetString("CartSessionKey");

            var cart = session.GetString("Cart");

            var cartItems = JsonConvert.DeserializeObject<List<CartItem>>(cart);

            if (cartItems == null || cartItems.Count == 0)
            {
                throw new NotFoundException("The cart is empty.");
            }

            foreach (var cartItem in cartItems)
            {
                await _cartItemRepository.Create(cartItem);
            }

            var order = new Domain.Entities.Order()
            {
                Address = request.Address,
                IsPaid = false,
                CartItems = cartItems,
                CartTotal = CalculateCartTotal.Calculate(cartItems),
                City = request.City,
                Email = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email),
                FirstName = request.FirstName,
                LastName = request.LastName,
                OrderDate = DateTime.Now,
                OrderStatus = OrderStatus.Pending,
                PhoneNumber = request.PhoneNumber,
                PostalCode = request.PostalCode,
                Street = request.Street
            };

            foreach (var item in cartItems)
            {
                if (item.Item.StockQuantity < item.Quantity || item.Item.StockQuantity < 0 || item.Quantity <= 0)
                {
                    throw new OutOfStockException("There are not that many items in stock.");
                }

                item.Item.StockQuantity = item.Item.StockQuantity - item.Quantity;
            }

            var serializedCartItems = JsonConvert.SerializeObject(new List<CartItem>());
            session.SetString("Cart", serializedCartItems);

            await _orderRepository.Create(order);

            return Unit.Value;
        }
    }
}
