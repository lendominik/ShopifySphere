using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Order.Commands.CreateOrder
{
    public static class CalculateCartTotal
    {
        public static decimal Calculate(List<CartItem> cartItems)
        {
            if (cartItems == null)
            {
                return 0;
            }

            decimal total = cartItems.Sum(item => item.UnitPrice);

            return total;
        }
    }
}
