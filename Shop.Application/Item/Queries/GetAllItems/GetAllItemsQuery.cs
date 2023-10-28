using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Item.Queries.GetAllItems
{
    public class GetAllItemsQuery : IRequest<PagedResult>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? SearchPhrase { get; set; }
    }
}
