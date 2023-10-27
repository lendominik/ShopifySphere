using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Item.Queries.GetItem
{
    public class GetItemQuery : IRequest<ItemDto>
    {
        public string EncodedName { get; set; }
        public GetItemQuery(string encodedName)
        {
            EncodedName = encodedName;
        }
    }
}
