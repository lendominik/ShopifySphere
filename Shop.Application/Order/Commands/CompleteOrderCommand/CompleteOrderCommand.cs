using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Order.Commands.CompleteOrderCommand
{
    public class CompleteOrderCommand : IRequest
    {
        public int OrderId { get; set; }
    }
}
