﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Category.Commands.DeleteCategory;
using Shop.Application.Item.Commands.DeleteItem;
using Shop.Application.Item.Commands.EditItem;
using Shop.Application.Item.Queries.GetAllItems;
using Shop.Application.Item.Queries.GetItem;

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
        public async Task<IActionResult> Index()
        {
            var items = await _mediator.Send(new GetAllItemsQuery());

            return View(items);
        }
        [Route("Item/{encodedName}/Edit")]
        public async Task<IActionResult> Edit(string encodedName)
        {
            var dto = await _mediator.Send(new GetItemQuery(encodedName));

            EditItemCommand model = _mapper.Map<EditItemCommand>(dto);

            return View(model);
        }
        [HttpPost]
        [Route("Item/{encodedName}/Edit")]
        public async Task<IActionResult> Edit(EditItemCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _mediator.Send(command);

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [Route("Item/Delete")]
        public async Task<IActionResult> Delete(DeleteItemCommand command)
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