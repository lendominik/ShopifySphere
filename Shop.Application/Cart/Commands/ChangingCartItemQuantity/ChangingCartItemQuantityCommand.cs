using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Cart.Commands.ChangingCartItemQuantity
{
    public class ChangingCartItemQuantityCommand : IRequest
    {
        public int Id { get; set; } // id cardItem
        public int Quantity { get; set; } // new quantity
    }
}
