using Microsoft.AspNetCore.Identity;
using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Cart
{
    public class CartDto
    {
        public string CartTotal { get; set; } // suma zamówienia
        public IdentityUser? OrderedBy { get; set; }
        public List<CartItem> CartItems { get; set; } 
    }
}
