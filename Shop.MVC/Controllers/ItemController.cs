using AutoMapper;
using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Category.Commands.DeleteCategory;
using Shop.Application.Item.Commands.DeleteItem;
using Shop.Application.Item.Commands.EditItem;
using Shop.Application.Item.Queries.GetAllItems;
using Shop.Application.Item.Queries.GetItem;
using Shop.MVC.Extensions;

namespace Shop.MVC.Controllers
{
    public class ItemController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ItemController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(int PageNumber, int PageSize, string searchPhrase, string SortBy, string SortDirection, string SelectedCategory)
        {
            var items = await _mediator.Send(new GetAllItemsQuery
            {
                PageNumber = PageNumber,
                PageSize = PageSize,
                SearchPhrase = searchPhrase,
                SortBy = SortBy,
                SortDirection = SortDirection,
                SelectedCategory = SelectedCategory
            });

            return View(items);
        }
        [Authorize(Roles = "Owner")]
        [Route("Item/{encodedName}/Edit")]
        public async Task<IActionResult> Edit(string encodedName)
        {
            var dto = await _mediator.Send(new GetItemQuery(encodedName));

            EditItemCommand model = _mapper.Map<EditItemCommand>(dto);

            return View(model);
        }
        [Route("Item/{encodedName}/Details")]
        public async Task<IActionResult> Details(string encodedName)
        {
            var item = await _mediator.Send(new GetItemQuery(encodedName));

            return View(item);
        }
        [Authorize(Roles = "Owner")]
        [HttpPost]
        [Route("Item/{encodedName}/Edit")]
        public async Task<IActionResult> Edit(EditItemCommand command)
        {
            if (!ModelState.IsValid)
            {
                this.SetNotification("error", $"Nie udało się zeedytować przedmiotu.");
                return BadRequest(ModelState);
            }

            this.SetNotification("warning", $"Zmieniono wartości przedmiotu.");
            await _mediator.Send(command);

            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Owner")]
        [HttpPost]
        [Route("Item/Delete")]
        public async Task<IActionResult> Delete(DeleteItemCommand command)
        {
            if (!ModelState.IsValid)
            {
                this.SetNotification("error", $"Nie udało się usunąć przedmiotu.");
                return BadRequest(ModelState);
            }

            await _mediator.Send(command);
            this.SetNotification("warning", $"Usunięto przedmiot.");

            return RedirectToAction(nameof(Index));
        }
    }
}
