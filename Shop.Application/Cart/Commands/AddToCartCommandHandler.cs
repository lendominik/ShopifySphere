using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Cart.Commands
{
    public class AddToCartCommandHandler : IRequestHandler<AddToCartCommand>
    {
        public AddToCartCommandHandler()
        {
            
        }

        public async Task<Unit> Handle(AddToCartCommand request, CancellationToken cancellationToken)
        {
            return Unit.Value;
        }
    }
}
