using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Cart.Queries.GetCart;
using Shop.Application.Item.Commands.DeleteItem;
using Shop.Application.Item.Queries.GetItem;
using Shop.Application.Order.Commands.CancelOrder;
using Shop.Application.Order.Commands.CompleteOrderCommand;
using Shop.Application.Order.Commands.ShipOrder;
using Shop.Application.Order.Queries.GetAllOrders;
using Shop.Application.Order.Queries.GetUserOrders;
using Shop.Application.Order.Queries.OrderDetails;
using Shop.Domain.Entities;
using Shop.MVC.Extensions;
using Stripe.Checkout;

namespace Shop.MVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public OrderController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var items = await _mediator.Send(new GetUserOrdersQuery());

            return View(items);
        }
        [Authorize(Roles = "Owner")]
        [Route("Order/All")]
        public async Task<IActionResult> AllOrders()
        {
            var items = await _mediator.Send(new GetAllOrdersQuery());

            return View(items);
        }
        [Authorize]
        [HttpPost]
        [Route("Order/CancelUserOrder")]
        public async Task<IActionResult> CancelUserOrder(CancelOrderCommand command)
        {
            if (!ModelState.IsValid)
            {
                this.SetNotification("error", $"Nie udało się anulować zamówienia.");
                return BadRequest(ModelState);
            }

            await _mediator.Send(command);
            this.SetNotification("warning", $"Anulowano zamówienie.");

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [Route("Order/CancelOrderByAdmin")]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> CancelOrderByAdmin(CancelOrderCommand command)
        {
            if (!ModelState.IsValid)
            {
                this.SetNotification("error", $"Nie udało się anulować zamówienia.");
                return BadRequest(ModelState);
            }

            await _mediator.Send(command);
            this.SetNotification("warning", $"Anulowano zamówienie.");

            var items = await _mediator.Send(new GetAllOrdersQuery());

            return View("AllOrders", items);
        }
        [Authorize(Roles = "Owner")]
        [HttpPost]
        [Route("Order/Ship")]
        public async Task<IActionResult> Ship(ShipOrderCommand command)
        {
            if (!ModelState.IsValid)
            {
                this.SetNotification("error", $"Nie udało się oznaczyć jako wysłanego.");
                return BadRequest(ModelState);
            }

            await _mediator.Send(command);
            this.SetNotification("warning", $"Zamówienie oznaczono jako wysłane.");

            var items = await _mediator.Send(new GetAllOrdersQuery());

            return View("AllOrders", items);
        }
        public async Task<IActionResult> CheckOut(int orderId)
        {
            var query = new OrderDetailsQuery ( orderId );

            var orderDto = await _mediator.Send(query);

            var domain = "http://localhost:7109/";

            var productList = orderDto.CartItems;

            var options = new SessionCreateOptions
            {
                SuccessUrl = domain + "Order/Confirmation",
                CancelUrl = domain + "Order/Login",
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment"
            };

            foreach (var item in productList)
            {
                var sessionListItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)item.Item.Price * 100, 
                        Currency = "pln", 
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Item.Name,
                            Description = item.Item.Description
                        },
                    },
                    Quantity = item.Quantity
                };
                options.LineItems.Add(sessionListItem);
            }

            var service = new SessionService();
            Session session = service.Create(options);

            return Redirect(session.Url);
        }
        [Authorize(Roles = "Owner")]
        [HttpPost]
        [Route("Order/Complete")]
        public async Task<IActionResult> Complete(CompleteOrderCommand command)
        {
            if (!ModelState.IsValid)
            {
                this.SetNotification("error", $"Nie udało się oznaczyć jako zrealizowane.");
                return BadRequest(ModelState);
            }

            await _mediator.Send(command);
            this.SetNotification("warning", $"Zamówienie oznaczono jako zrealizowane.");

            var items = await _mediator.Send(new GetAllOrdersQuery());

            return View("AllOrders", items);
        }
        [Route("Order/{orderId}/Details")]
        public async Task<IActionResult> Details(int orderId)
        {
            var item = await _mediator.Send(new OrderDetailsQuery(orderId));

            return View(item);
        }
    }
}
