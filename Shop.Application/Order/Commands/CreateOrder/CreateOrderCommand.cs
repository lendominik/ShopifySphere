using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Order.Commands.CreateOrder
{
    public class CreateOrderCommand : OrderDto, IRequest
    {
    }
}
