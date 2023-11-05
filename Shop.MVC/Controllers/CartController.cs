using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Cart.Commands.AddToCart;
using Shop.Application.Cart.Commands.ChangingCartItemQuantity;
using Shop.Application.Cart.Commands.RemoveFromCart;
using Shop.Application.Cart.Queries.GetCart;
using Shop.Application.Category.Commands.CreateCategory;
using Shop.Application.Category.Commands.DeleteCategory;
using Shop.Application.Category.Queries.GetAllCategories;
using Shop.Application.Item.Commands.CreateItem;
using Shop.Application.Item.Commands.DeleteItem;
using Shop.Application.Item.Queries.GetItem;
using Shop.Application.Order.Commands.CreateOrder;
using Shop.Domain.Entities;
using Shop.MVC.Extensions;

namespace Shop.MVC.Controllers
{
    public class CartController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CartController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var items = await _mediator.Send(new GetCartQuery());

            return View(items);
        }

        [HttpPost]
        [Route("Cart/AddItem")]
        public async Task<IActionResult> Create(AddToCartCommand command)
        {
            if (command == null)
            {
                this.SetNotification("error", $"Wystąpił błąd podcas dodawania przedmiotu do koszyka.");
                return BadRequest(ModelState);
            }

            await _mediator.Send(command);
            this.SetNotification("success", $"Dodano przedmiot do koszyka.");

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Route("Cart/Details")]
        public async Task<IActionResult> CreateItem(AddToCartCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _mediator.Send(command);

            return RedirectToAction(nameof(Index));
        }
        [Authorize]
        public async Task<IActionResult> CreateOrder()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        [Route("Cart/CreateOrder")]
        public async Task<IActionResult> CreateOrder(string cartId, CreateOrderCommand command)
        {
            command.CartId = cartId;


            if (!ModelState.IsValid)
            {
                this.SetNotification("error", $"Wystąpił błąd podczas składania zamówienia.");
                return BadRequest(ModelState);
            }

            this.SetNotification("success", $"Zamówienie przyjęto do realizacji!");
            await _mediator.Send(command);

            var newOrderId = command.OrderId;

            return RedirectToAction("Details", "Order", new { orderId = newOrderId });
        }
        [Route("Cart/Details")]
        public async Task<IActionResult> Details(GetCartQuery command)
        {
            var cart = await _mediator.Send(command);

            return View(cart);
        }
        [Route("Cart/AddItem")]
        public async Task<IActionResult> AddItem(AddToCartCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _mediator.Send(command);

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [Route("Cart/DeleteItem")]
        public async Task<IActionResult> DeleteItem(RemoveFromCartCommand command)
        {
            if (!ModelState.IsValid)
            {
                this.SetNotification("error", $"Nie udało usunąć się przedmiotu z koszyka.");
                return BadRequest(ModelState);
            }

            this.SetNotification("success", $"Usunięto przedmiot z koszyka.");
            await _mediator.Send(command);

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [Route("Cart/UpdateItemQuantity")]
        public async Task<IActionResult> UpdateItemQuantity(ChangingCartItemQuantityCommand command)
        {
            if (!ModelState.IsValid)
            {
                this.SetNotification("error", $"Wystąpił błąd przy próbie zmiany liczby przedmiotów w koszyku.");
                return BadRequest(ModelState);
            }

            this.SetNotification("success", $"Zaktualizowano liczbę przedmiotów w koszyku.");
            await _mediator.Send(command);

            return RedirectToAction(nameof(Index));
        }
    }
}
