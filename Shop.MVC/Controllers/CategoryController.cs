
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Category.Commands.CreateCategory;
using Shop.Application.Category.Commands.DeleteCategory;
using Shop.Application.Category.Commands.EditCategory;
using Shop.Application.Category.Queries.GetAllCategories;
using Shop.Application.Category.Queries.GetCategory;
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
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [Route("Category/Create")]
        public async Task<IActionResult> Create(CreateCategoryCommand command)
        {
            if (!ModelState.IsValid || command == null)
            {
                return BadRequest(ModelState);
            }

            await _mediator.Send(command);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Route("Category/CreateItem")]
        public async Task<IActionResult> CreateItem(CreateItemCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _mediator.Send(command);

            return RedirectToAction(nameof(Index));
        }
        [Route("Category/{encodedName}/Edit")]
        public async Task<IActionResult> Edit(string encodedName)
        {
            var dto = await _mediator.Send(new GetCategoryQuery(encodedName));

            EditCategoryCommand model = _mapper.Map<EditCategoryCommand>(dto);

            return View(model);
        }
        [HttpPost]
        [Route("Category/{encodedName}/Edit")]
        public async Task<IActionResult> Edit(EditCategoryCommand command)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _mediator.Send(command);

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [Route("Category/Delete")]
        public async Task<IActionResult> Delete(DeleteCategoryCommand command)
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
