using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Abstract;
using Api.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentApi.Models;

namespace PaymentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IOrderRepository orderRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly ICardRepository cardRepository;
        private readonly IPaymentRepository paymentRepository;
        private List<string> transactions = new List<string>();


        public TransactionController(IOrderRepository orderRepository, ICustomerRepository customerRepository, ICardRepository cardRepository, IPaymentRepository paymentRepository)
        {
            this.orderRepository = orderRepository;
            this.customerRepository = customerRepository;
            this.cardRepository = cardRepository;
            this.paymentRepository = paymentRepository;
        }

        public int Get(int value, int storeId)
        {

            string response = value + "-" + storeId;

            if(response != null)
            {
                transactions.Add(response);
            }

            Order order = new Order()
            {
                amountOfMoney = value,
                merchantId = storeId.ToString()
            };

            orderRepository.SaveOrder(order);
            orderRepository.Commit();

            return order.Id;
        }

        [HttpPost]
        public bool Post(PaymentApiModel paymentData)
        {

           // Order order = paymentData.PaymentData.Order;

            if (paymentData.Card.Id == 0)
            {
                Card card = new Card()
                {
                    cardholderName = paymentData.Card.cardholderName,
                    cvv = paymentData.Card.cvv,
                    cardNumber = paymentData.Card.cardNumber,
                    expiryDate = paymentData.Card.expiryDate
                };

                cardRepository.SaveCard(card);
                cardRepository.Commit();
                
            }
            else
            {
                Card card = cardRepository.GetById(paymentData.Card.Id);
            }

            Customer customer = new Customer()
            {
                cardId = cardRepository.GetByCredentials(paymentData.Card.cardholderName, paymentData.Card.cvv, paymentData.Card.cardNumber).Id,
                Name = paymentData.Customer.Name
            };

            customerRepository.SaveCustomer(customer);
            customerRepository.Commit();

            //TODO : VOIR POURQUOI CET ENCULAY DE EF NE VEUX PAS METTRE A JOUR LORDERID VTFF IL EST 58
            PaymentData newPayment = new PaymentData() {

                
                Customer = customer,
                OrderId = paymentData.PaymentData.OrderId
            };

            paymentRepository.SavePayment(newPayment);
            paymentRepository.Commit();

            return true;
        }

    }
}