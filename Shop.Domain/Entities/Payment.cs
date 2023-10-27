using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentAmount { get; set; }
        public DateTime PaymentDate { get; set; }
        public Order Order { get; set; }
        public int OrderId { get; set; }

    }
}
