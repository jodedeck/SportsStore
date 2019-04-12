using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Payment.Models
{


    public class PaymentJsonApi
    {
        public Order order { get; set; }
        public Cardpaymentmethodspecificinput cardPaymentMethodSpecificInput { get; set; }
    }

    public class Order
    {
        public Amountofmoney amountOfMoney { get; set; }
        public Customer customer { get; set; }
        public References references { get; set; }
        public Item[] items { get; set; }
    }

    public class Amountofmoney
    {
        public string currencyCode { get; set; }
        public int amount { get; set; }
    }

    public class Customer
    {
        public string merchantCustomerId { get; set; }
        public Personalinformation personalInformation { get; set; }
        public Companyinformation companyInformation { get; set; }
        public string languageCode { get; set; }
        public Billingaddress billingAddress { get; set; }
        public Shippingaddress shippingAddress { get; set; }
        public Contactdetails contactDetails { get; set; }
        public string vatNumber { get; set; }
    }

    public class Personalinformation
    {
        public Name name { get; set; }
        public string gender { get; set; }
        public string dateOfBirth { get; set; }
    }

    public class Name
    {
        public string title { get; set; }
        public string firstName { get; set; }
        public string surnamePrefix { get; set; }
        public string surname { get; set; }
    }

    public class Companyinformation
    {
        public string name { get; set; }
    }

    public class Billingaddress
    {
        public string street { get; set; }
        public string houseNumber { get; set; }
        public string additionalInfo { get; set; }
        public string zip { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string countryCode { get; set; }
    }

    public class Shippingaddress
    {
        public Name1 name { get; set; }
        public string street { get; set; }
        public string houseNumber { get; set; }
        public string additionalInfo { get; set; }
        public string zip { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string countryCode { get; set; }
    }

    public class Name1
    {
        public string title { get; set; }
        public string firstName { get; set; }
        public string surname { get; set; }
    }

    public class Contactdetails
    {
        public string emailAddress { get; set; }
        public string phoneNumber { get; set; }
        public string faxNumber { get; set; }
        public string emailMessageType { get; set; }
    }

    public class References
    {
        public string merchantOrderId { get; set; }
        public string merchantReference { get; set; }
        public Invoicedata invoiceData { get; set; }
        public string descriptor { get; set; }
    }

    public class Invoicedata
    {
        public string invoiceNumber { get; set; }
        public string invoiceDate { get; set; }
    }

    public class Item
    {
        public Amountofmoney1 amountOfMoney { get; set; }
        public Invoicedata1 invoiceData { get; set; }
    }

    public class Amountofmoney1
    {
        public string currencyCode { get; set; }
        public int amount { get; set; }
    }

    public class Invoicedata1
    {
        public string nrOfItems { get; set; }
        public int pricePerItem { get; set; }
        public string description { get; set; }
    }

    public class Cardpaymentmethodspecificinput
    {
        public int paymentProductId { get; set; }
        public bool skipAuthentication { get; set; }
        public Card card { get; set; }
    }

    public class Card
    {
        public string cvv { get; set; }
        public string cardNumber { get; set; }
        public string expiryDate { get; set; }
        public string cardholderName { get; set; }
    }

}
