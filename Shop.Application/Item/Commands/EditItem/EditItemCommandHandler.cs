using MediatR;
using Shop.Application.ApplicationUser;
using Shop.Application.Exceptions;
using Shop.Application.Services;
using Shop.Domain.Interfaces;

namespace Shop.Application.Item.Commands.EditItem
{
    public class EditItemCommandHandler : IRequestHandler<EditItemCommand>
    {
        private readonly IItemRepository _itemRepository;
        private readonly IAccessControlService _accessControlService;
        private readonly IUserContext _userContext;

        public EditItemCommandHandler(IUserContext userContext, IItemRepository itemRepository, IAccessControlService accessControlService)
        {
            _itemRepository = itemRepository;
            _accessControlService = accessControlService;
            _userContext = userContext;
        }

        public async Task<Unit> Handle(EditItemCommand request, CancellationToken cancellationToken)
        {
            if (!_accessControlService.IsEditable(_userContext))
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
