using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Item.Commands.CreateItem
{
    public class CreateItemCommand : ItemDto ,IRequest
    {
        public IFormFile Image { get; set; }
        public string CategoryEncodedName { get; set; }
    }
}
