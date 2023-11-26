using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Entities
{
    public class CartItem
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string CartId { get; set; }
        public Item Item { get; set; }
        public int ItemId { get; set; }
    }
}
