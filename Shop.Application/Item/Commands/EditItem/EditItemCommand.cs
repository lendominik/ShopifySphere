using MediatR;
using Microsoft.AspNetCore.Http;

namespace Shop.Application.Item.Commands.EditItem
{
    public class EditItemCommand : IRequest
    {
        public string EncodedName { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int StockQuantity { get; set; }
        public IFormFile Image { get; set; }
    }
}
