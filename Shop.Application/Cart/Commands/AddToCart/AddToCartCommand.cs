﻿using MediatR;

namespace Shop.Application.Cart.Commands.AddToCart
{
    public class AddToCartCommand : IRequest
    {
        public int Quantity { get; set; }
        public float UnitPrice { get; set; }
        public string EncodedName { get; set; }
    }
}
