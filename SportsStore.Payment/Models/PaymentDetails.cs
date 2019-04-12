using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Payment.Models
{
    public class PaymentDetails
    {

        public PaymentDetails()
        {

        }
        public PaymentDetails(string transactionId)
        {
            TransactionId = transactionId;
        }

        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public DateTime ExpirationDate { get; set; }
        [Required]
        public double CardNumber { get; set; }
        [Required]
        public int SecurityNumber { get; set; }

       // public string Price { get; set; }

        public string TransactionId { get; set; }

    }
}
