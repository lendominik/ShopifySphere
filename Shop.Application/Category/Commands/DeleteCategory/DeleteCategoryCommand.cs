using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Category.Commands.DeleteCategory
{
    public class DeleteCategoryCommand : IRequest
    {
        public string EncodedName { get; set; }
    }
}
