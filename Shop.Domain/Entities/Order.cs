using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } // data złożenia
        public string Status { get; set; } // status zamówienia
        public string ShippingInformation { get; set; } // informacje o dostawie
        public string OrderTotal { get; set; } // suma zamówienia
        public IdentityUser? OrderedBy { get; set; }
        public Payment Payment { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    }
}
