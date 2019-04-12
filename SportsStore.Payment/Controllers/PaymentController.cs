using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Api.Data.Abstract;
using Api.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Payment.Models;
using System.Text;
using Newtonsoft.Json;
using PaymentApi.Models;

namespace SportsStore.Payment.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IOrderRepository orderRepository;
        private readonly ICardRepository cardRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly IPaymentRepository paymentRepository;
        private string successUrl = "http://localhost:50036/Cart/SuccessPayment";
        private string failureUrl = "http://localhost:50036/Cart/FailPayment";



        public PaymentController(IOrderRepository orderRepository, ICardRepository cardRepository, ICustomerRepository customerRepository, IPaymentRepository paymentRepository)
        {
            this.orderRepository = orderRepository;
            this.cardRepository = cardRepository;
            this.customerRepository = customerRepository;
            this.paymentRepository = paymentRepository;
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
          

            Api.Data.Entities.Card card = RetrieveCard(payementDetails);
      

            //cardRepository.SaveCard(card);
            //cardRepository.Commit();

            Api.Data.Entities.Customer customer = new Api.Data.Entities.Customer
            {
                cardId = card.Id,
                Name = payementDetails.Lastname +", " +payementDetails.Firstname
            };



            //customerRepository.SaveCustomer(customer);
            //customerRepository.Commit();

            PaymentData payment = new PaymentData()
            {
                Order = orderRepository.GetById(int.Parse(payementDetails.TransactionId)),
                Customer = customer
            };

            //paymentRepository.SavePayment(payment);
            //paymentRepository.Commit();

            PaymentApiModel pam = new PaymentApiModel(card, payment, customer);

            var requestResult = CreatePaymentRequest(pam);




            //ajouter une loguique de réussite ou de rejet du payment
            //manipuler le paymentDetails pour voir si ca marche
            bool result = Validate(payementDetails);
            var rnd = new Random();
            int val = rnd.Next(6);

         
         


            //Si le payment réussi, alors on renvoi vers l'écran de réussite
            if (result && val <= 3)
            {
                return Redirect(successUrl);
            }
            else
                return Redirect(failureUrl);
        }

        private Api.Data.Entities.Card RetrieveCard(PaymentDetails payementDetails)
        {
            string month = payementDetails.ExpirationDate.Month.ToString();
            string year = payementDetails.ExpirationDate.Year.ToString();
            string expDate = month + "/" + year;

            if (cardRepository.GetByCredentials(payementDetails.Lastname, payementDetails.SecurityNumber.ToString(), payementDetails.CardNumber.ToString()) == null)
            {
                Api.Data.Entities.Card card = new Api.Data.Entities.Card
                {
                    cardholderName = payementDetails.Lastname,
                    cardNumber = payementDetails.CardNumber.ToString(),
                    cvv = payementDetails.SecurityNumber.ToString(),
                    expiryDate = expDate
                };
                return card;
            }
            else
            {
                return cardRepository.GetByCredentials(payementDetails.Lastname, payementDetails.SecurityNumber.ToString(), payementDetails.CardNumber.ToString());
            }
        }

        private async Task<HttpResponseMessage> CreatePaymentRequest(PaymentApiModel payment)
        {
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri("https://localhost:44377/api/Transaction");


                var json = JsonConvert.SerializeObject(payment);
                request.Content = new StringContent( json , Encoding.UTF8, "application/json");
                var response = await client.SendAsync(request);
                return response;
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