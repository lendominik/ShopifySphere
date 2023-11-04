using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Order.Commands.SetOrderPaidStatus
{
    public class SetOrderPaidStatusCommand : IRequest
    {
        public int OrderId { get; set; }
    }
}
