using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Cart.Queries.GetCart;
using Shop.Application.Item.Commands.DeleteItem;
using Shop.Application.Order.Commands.CancelOrder;
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
        [HttpPost]
        [Route("Order/Cancel")]
        public async Task<IActionResult> Cancel(CancelOrderCommand command)
        {
            if (!ModelState.IsValid)
            {
                this.SetNotification("error", $"Nie udało się anulować zamówienia.");
                return BadRequest(ModelState);
            }

            await _mediator.Send(command);
            this.SetNotification("warning", $"Anulowano zamówienie..");

            return RedirectToAction(nameof(Index));
        }
    }
}
