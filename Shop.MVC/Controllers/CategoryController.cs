
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Category.Queries.GetAllCategories;
using Shop.Application.Item.Commands.CreateItem;
using Shop.Application.Item.Queries.GetAllItems;

namespace Shop.MVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CategoryController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await _mediator.Send(new GetAllCategoriesQuery());

            return View(categories);
        }
        [HttpPost]
        [Route("Category/CreateItem")]
        public async Task<IActionResult> CreateItem(CreateItemCommand command)
        {
            await Console.Out.WriteLineAsync("ASDDDDDDDDDDDDDDDDDDDDDDDDDDDDDd");
            await Console.Out.WriteLineAsync(command.CategoryEncodedName);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _mediator.Send(command);

            return RedirectToAction(nameof(Index));
        }
    }
}
