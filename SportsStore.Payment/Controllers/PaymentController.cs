using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Api.Data.Abstract;
using Api.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Payment.Models;
using Api.Data.Entities;


namespace SportsStore.Payment.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IOrderRepository orderRepository;
        private string successUrl = "http://localhost:50036/Cart/SuccessPayment";
        private string failureUrl = "http://localhost:50036/Cart/FailPayment";



        public PaymentController(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public IActionResult Create()
        {
            string text = Request.Path;
            string transactionid = text.Split('/').Last();

            //int index = transactionId.IndexOf("-");
            //string price = transactionId.Substring(0, index);
            //string shopId = transactionId.Substring(index + 1);

            PaymentDetails pd = new PaymentDetails()
            {
                TransactionId = transactionid
            };

            return View(pd);
        }

        [HttpPost]
        public RedirectResult Create(PaymentDetails payementDetails)
        {
            Api.Data.Entities.Card card = new Api.Data.Entities.Card
            {

            };

            PaymentData payment = new PaymentData()
            {
                Order = orderRepository.GetById(int.Parse(payementDetails.TransactionId)),
                

                Customer = new Api.Data.Entities.Customer
                {
                    cardId = 0,
                    Name = ""
                }
               

            };







            //ajouter une loguique de réussite ou de rejet du payment
            //manipuler le paymentDetails pour voir si ca marche
            bool result = Validate(payementDetails);
            var rnd = new Random();
            int val = rnd.Next(6);

            //créer une requête avec le transactionId et les info de payment vers l'api
            bool isPaid = SendPayment(payementDetails);


            //Si le payment réussi, alors on renvoi vers l'écran de réussite
            if (result && val <= 3)
            {
                return Redirect(successUrl);
            }
            else
                return Redirect(failureUrl);
        }

        private bool SendPayment(PaymentDetails payementDetails)
        {

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                return true;
            }
        }

        private bool Validate(PaymentDetails payementDetails)
        {
            if(string.IsNullOrEmpty(payementDetails.Firstname) || string.IsNullOrWhiteSpace(payementDetails.Firstname))
            {
                throw new FormatException("Please enter a First name");
                
            }

            if (string.IsNullOrEmpty(payementDetails.Lastname) || string.IsNullOrWhiteSpace(payementDetails.Lastname))
            {
                throw new FormatException("Please enter a Last name");
            }

            if(payementDetails.ExpirationDate <= DateTime.Now)
            {
                return false;
            }

            if(payementDetails.SecurityNumber.ToString().Count() != 3)
            {
                return false;
            }

            return true;

        }
    }
}