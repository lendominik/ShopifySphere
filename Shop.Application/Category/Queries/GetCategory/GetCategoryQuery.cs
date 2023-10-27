using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Category.Queries.GetCategory
{
    public class GetCategoryQuery : IRequest<CategoryDto>
    {
        public string EncodedName { get; set; }
        public GetCategoryQuery(string encodedName)
        {
            EncodedName = encodedName;
        }
    }
}
