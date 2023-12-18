using MediatR;
using Shop.Application.ApplicationUser;
using Shop.Application.Exceptions;
using Shop.Domain.Interfaces;

namespace Shop.Application.Item.Commands.DeleteItem
{
    public class DeleteItemCommandHandler : IRequestHandler<DeleteItemCommand>
    {
        private readonly IItemRepository _itemRepository;
        private readonly IUserContext _userContext;

        public DeleteItemCommandHandler(IUserContext userContext, IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
            _userContext = userContext;
        }

        public async Task<Unit> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
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

            await _itemRepository.Delete(item);
            return Unit.Value;
        }
    }
}
