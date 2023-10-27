using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Item
{
    public class ItemDto
    {
        public string Name { get; set; } // nazwa kategorii
        public string Price { get; set; } // cena
        public string Description { get; set; } //opis 
        public int StockQuantity { get; set; } // stan magazynowy
        public string ProductImage { get; set; } // zdjecie produktu\
        public Domain.Entities.Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}
