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
        public decimal CartTotal { get; set; }
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public bool IsPaid { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
    }
}
