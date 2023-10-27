using MediatR;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Item.Queries
{
    public class GetAllItemsQuery : IRequest<IEnumerable<ItemDto>>
    {
    }
}
