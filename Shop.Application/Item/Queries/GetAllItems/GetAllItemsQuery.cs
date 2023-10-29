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
        public string SortBy { get; set; }
        public string SortDirection { get; set; }
        public string Categories { get; set; }
        public string SelectedCategory { get; set; }
    }
}
