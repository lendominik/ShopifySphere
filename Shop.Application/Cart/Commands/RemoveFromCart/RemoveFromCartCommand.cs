using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Cart.Commands.RemoveFromCart
{
    public class RemoveFromCartCommand : IRequest
    {
        public int Id { get; set; } // id cardItem
    }
}
