using MediatR;

namespace Shop.Application.Item.Queries.GetItem
{
    public class GetItemQuery : IRequest<ItemDto>
    {
        public string EncodedName { get; set; }
        public GetItemQuery(string encodedName)
        {
            EncodedName = encodedName;
        }
    }
}
