using MediatR;
using Shop.Application.Exceptions;
using Shop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Item.Commands.EditItem
{
    public class EditItemCommandHandler : IRequestHandler<EditItemCommand>
    {
        private readonly IItemRepository _itemRepository;

        public EditItemCommandHandler(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<Unit> Handle(EditItemCommand request, CancellationToken cancellationToken)
        {
            var item = await _itemRepository.GetByEncodedName(request.EncodedName);

            if (item == null)
            {
                throw new NotFoundException("Podany przedmiot nie istnieje.");
            }

            item.Description = request.Description;
            item.Price = request.Price;
            item.StockQuantity = request.StockQuantity;
            item.ProductImage = request.ProductImage;

            await _itemRepository.Commit();

            return Unit.Value;
        }
    }
}
