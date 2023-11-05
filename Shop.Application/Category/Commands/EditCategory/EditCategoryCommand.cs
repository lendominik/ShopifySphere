using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Category.Commands.EditCategory
{
    public class EditCategoryCommand : IRequest
    {
        public string Description { get; set; }
        public string EncodedName { get; set; }
    }
}
