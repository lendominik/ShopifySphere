using MediatR;
using Microsoft.AspNetCore.Http;

namespace Shop.Application.Item.Commands.CreateItem
{
    public class CreateItemCommand : ItemDto ,IRequest
    {
        public IFormFile Image { get; set; }
        public string CategoryEncodedName { get; set; }
    }
}
