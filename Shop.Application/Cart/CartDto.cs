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
        public string Id { get; set; }
        public float CartTotal { get; set; } // suma zamówienia
        public List<CartItem> CartItems { get; set; } 
    }
}
