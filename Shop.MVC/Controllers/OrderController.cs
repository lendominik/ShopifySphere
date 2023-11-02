using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Cart.Queries.GetCart;
using Shop.Application.Item.Commands.DeleteItem;
using Shop.Application.Order.Commands.CancelOrder;
using Shop.Application.Order.Commands.CompleteOrderCommand;
using Shop.Application.Order.Commands.ShipOrder;
using Shop.Application.Order.Queries.GetAllOrders;
using Shop.Application.Order.Queries.GetUserOrders;
using Shop.MVC.Extensions;

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
    }
}
