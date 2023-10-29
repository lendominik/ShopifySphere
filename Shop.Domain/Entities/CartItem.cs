﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Entities
{
    public class CartItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string UnitPrice { get; set; }
        public Cart Cart { get; set; }
        public int CartId { get; set; }
        public Item Item { get; set; }
        public int ItemId { get; set; }

    }
}
