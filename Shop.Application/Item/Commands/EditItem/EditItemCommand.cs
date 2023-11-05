using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Item.Commands.EditItem
{
    public class EditItemCommand : IRequest
    {
        public string EncodedName { get; set; }
        public decimal Price { get; set; } // cena
        public string Description { get; set; } //opis 
        public int StockQuantity { get; set; } // stan magazynowy
        public IFormFile Image { get; set; }
    }
}
