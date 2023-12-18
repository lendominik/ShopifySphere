﻿namespace Shop.Domain.Entities
{
    public enum OrderStatus
    {
        Pending, 
        Shipped,
        Delivered, 
        Cancelled 
    }
    public class Order
    {
        public int Id { get; set; }
        public decimal CartTotal { get; set; }
        public List<CartItem> CartItems { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public bool IsPaid { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
