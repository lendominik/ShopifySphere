
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Category.Commands.CreateCategory;
using Shop.Application.Category.Commands.DeleteCategory;
using Shop.Application.Category.Commands.EditCategory;
using Shop.Application.Category.Queries.GetAllCategories;
using Shop.Application.Category.Queries.GetCategory;
using Shop.Application.Item.Commands.CreateItem;
using Shop.Application.Item.Queries.GetAllItems;
using Shop.Domain.Entities;
using Shop.MVC.Extensions;

namespace Shop.MVC.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CategoryController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var categories = await _mediator.Send(new GetAllCategoriesQuery());

            return View(categories);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [Authorize(Roles = "Owner")]
        [HttpPost]
        [Route("Category/Create")]
        public async Task<IActionResult> Create(CreateCategoryCommand command)
        {
            if (!ModelState.IsValid || command == null)
            {
                this.SetNotification("error", $"Nie udało się dodać nowej kategorii.");
                return BadRequest(ModelState);
            }

            this.SetNotification("success", $"Dodano nową kategorię.");
            await _mediator.Send(command);

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Owner")]
        [HttpPost]
        [Route("Category/CreateItem")]
        public async Task<IActionResult> CreateItem(CreateItemCommand command)
        {

            if (!ModelState.IsValid)
            {
                this.SetNotification("error", $"Nie udało się dodać nowego przedmiotu do wybranej kategorii");
                return BadRequest(ModelState);
            }

            this.SetNotification("success", $"Dodano nowy przedmiot do wybranej kategorii");
            await _mediator.Send(command);

            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Owner")]
        [Route("Category/{encodedName}/Edit")]
        public async Task<IActionResult> Edit(string encodedName)
        {
            var dto = await _mediator.Send(new GetCategoryQuery(encodedName));

            EditCategoryCommand model = _mapper.Map<EditCategoryCommand>(dto);

            return View(model);
        }
        [Authorize(Roles = "Owner")]
        [HttpPost]
        [Route("Category/{encodedName}/Edit")]
        public async Task<IActionResult> Edit(EditCategoryCommand command)
        {
            if(!ModelState.IsValid)
            {
                this.SetNotification("error", $"Nie udało się dokonanie edycji wybranej kategorii.");
                return BadRequest(ModelState);
            }

            this.SetNotification("success", $"Dokonano edycji wybranej kategorii.");
            await _mediator.Send(command);

            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Owner")]
        [HttpPost]
        [Route("Category/Delete")]
        public async Task<IActionResult> Delete(DeleteCategoryCommand command)
        {
            if (!ModelState.IsValid)
            {
                this.SetNotification("error", $"Wystąpił błąd przy usuwaniu kategorii.");
                return BadRequest(ModelState);
            }

            await _mediator.Send(command);
            this.SetNotification("warning", $"Usunięto wybraną kategorię.");

            return RedirectToAction(nameof(Index));
        }
    }
}
