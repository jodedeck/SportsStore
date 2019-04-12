using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Data.Entities
{
    public class Card
    {
        public int Id { get; set; }
        public string cvv { get; set; }
        public string cardNumber { get; set; }
        public string expiryDate { get; set; }
        public string cardholderName { get; set; }
    }
}
