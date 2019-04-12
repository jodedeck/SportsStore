using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Data.Entities
{
    public class PaymentData
    {
        public int Id { get; set; }

        public Order Order { get; set; }
        public Customer Customer { get; set; }
    }
}
