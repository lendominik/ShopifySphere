using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Item.Commands.EditItem
{
    public class EditItemCommand : ItemDto, IRequest
    {
    }
}
