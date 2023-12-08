﻿using MediatR;
using Microsoft.AspNetCore.Hosting;
using Shop.Application.Exceptions;
using Shop.Domain.Interfaces;

namespace Shop.Application.Item.Commands.EditItem
{
    public class EditItemCommandHandler : IRequestHandler<EditItemCommand>
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IItemRepository _itemRepository;

        public EditItemCommandHandler(IItemRepository itemRepository, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _itemRepository = itemRepository;
        }

        public async Task<Unit> Handle(EditItemCommand request, CancellationToken cancellationToken)
        {
            var item = await _itemRepository.GetByEncodedName(request.EncodedName);


            if (item == null)
            {
                throw new NotFoundException("Item not found.");
            }

            item.Description = request.Description;
            item.Price = request.Price;
            item.StockQuantity = request.StockQuantity;

            await _itemRepository.Commit();

            return Unit.Value;
        }
    }
}
