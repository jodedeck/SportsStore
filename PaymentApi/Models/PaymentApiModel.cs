using Api.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApi.Models
{
    public class PaymentApiModel
    {

        public PaymentApiModel()
        {

        }

        public PaymentApiModel(Card card, PaymentData paymentData, Customer customer)
        {
            Card = card;
            PaymentData = paymentData;
            Customer = customer;
        }
        public Card Card { get; set; }
        public PaymentData PaymentData { get; set; }

        public Customer Customer { get; set; }

    }
}
