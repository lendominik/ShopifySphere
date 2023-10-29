using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Cart.Commands
{
    public class AddToCartCommand : IRequest
    {
        public string ItemEncodedName { get; set; }
    }
}
