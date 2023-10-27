using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string UnitPrice { get; set; }
        public Order Order { get; set; }
        public int OrderId { get; set; }
        public Item Item { get; set; }
        public int ItemId { get; set; }
    }
}
