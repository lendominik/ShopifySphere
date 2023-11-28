using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Order.Commands.CancelOrder;
using Shop.Application.Order.Commands.CompleteOrderCommand;
using Shop.Application.Order.Commands.SetOrderPaidStatus;
using Shop.Application.Order.Commands.ShipOrder;
using Shop.Application.Order.Queries.GetAllOrders;
using Shop.Application.Order.Queries.GetPaymentSession;
using Shop.Application.Order.Queries.GetUserOrders;
using Shop.Application.Order.Queries.OrderDetails;
using Shop.MVC.Extensions;
using Stripe.Checkout;

namespace Shop.MVC.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> AllOrders(int PageNumber, int PageSize, string searchPhrase, string orderStatus)
        {
            var orders = await _mediator.Send(new GetAllOrdersQuery
            {
                PageNumber = PageNumber,
                PageSize = PageSize,
                SearchPhrase = searchPhrase,
                Status = orderStatus
            });

            return View(orders);
        }
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
        [Authorize(Roles = "Owner")]
        [HttpPost]
        [Route("Order/CancelOrderByAdmin")]
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
        [Route("Order/Success")]
        public async Task<IActionResult> Success()
        {
            if (TempData.ContainsKey("Session") && TempData["Session"] != null)
            {
                var sessionId = TempData["Session"].ToString();
                var service = new SessionService();

                if (!string.IsNullOrEmpty(sessionId))
                {
                    Session session = service.Get(sessionId);

                    var orderId = (int)TempData["OrderId"];

                    var query = new SetOrderPaidStatusCommand()
                    {
                        OrderId = orderId
                    };

                    await _mediator.Send(query);

                    return View("Success");
                }
            }

            return View("Cancel");
        }
        [Route("Order/Cancel")]
        public IActionResult Cancel()
        {
            return View("Cancel");
        }
        public async Task<IActionResult> CheckOut(int orderId)
        {
            var query = new GetPaymentSessionQuery
            {
                OrderId = orderId
            };

            var session = await _mediator.Send(query);

            TempData["Session"] = session.Id;
            TempData["OrderId"] = orderId;

            Response.Headers.Add("Location", session.Url);

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
