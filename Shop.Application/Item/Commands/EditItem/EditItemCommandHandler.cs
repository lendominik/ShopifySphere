using MediatR;
using Microsoft.AspNetCore.Hosting;
using Shop.Application.ApplicationUser;
using Shop.Application.Exceptions;
using Shop.Domain.Interfaces;

namespace Shop.Application.Item.Commands.EditItem
{
    public class EditItemCommandHandler : IRequestHandler<EditItemCommand>
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IItemRepository _itemRepository;
        private readonly IUserContext _userContext;

        public EditItemCommandHandler(IUserContext userContext, IItemRepository itemRepository, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _itemRepository = itemRepository;
        }

        public async Task<Unit> Handle(EditItemCommand request, CancellationToken cancellationToken)
        {
            var user = _userContext.GetCurrentUser();
            var isEdibable = user != null && (user.IsInRole("Owner"));

            if (!isEdibable)
            {
                return Unit.Value;
            }

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
