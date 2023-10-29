using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Cart.Commands.AddToCart;
using Shop.Application.Cart.Queries.GetCart;
using Shop.Application.Category.Commands.CreateCategory;
using Shop.Application.Category.Queries.GetAllCategories;
using Shop.Application.Item.Commands.CreateItem;
using Shop.Application.Item.Queries.GetItem;

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
                return BadRequest(ModelState);
            }

            await _mediator.Send(command);

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
    }
}
