using MediatR;

namespace Shop.Application.Item.Commands.DeleteItem
{
    public class DeleteItemCommand : IRequest
    {
        public string EncodedName { get; set; }
    }
}
