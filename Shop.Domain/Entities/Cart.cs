using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Entities
{
    public class Cart
    {
        public string Id { get; set; }
        public string? CartTotal { get; set; }
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
