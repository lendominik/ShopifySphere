using MediatR;
using Shop.Application.ApplicationUser;
using Shop.Application.Exceptions;
using Shop.Application.Services;
using Shop.Domain.Interfaces;

namespace Shop.Application.Item.Commands.DeleteItem
{
    public class DeleteItemCommandHandler : IRequestHandler<DeleteItemCommand>
    {
        private readonly IItemRepository _itemRepository;
        private readonly IAccessControlService _accessControlService;
        private readonly IUserContext _userContext;

        public DeleteItemCommandHandler(IAccessControlService accessControlService, IItemRepository itemRepository, IUserContext userContext)
        {
            _itemRepository = itemRepository;
            _accessControlService = accessControlService;
            _userContext = userContext;
        }

        public async Task<Unit> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
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

            await _itemRepository.Delete(item);
            return Unit.Value;
        }
    }
}
