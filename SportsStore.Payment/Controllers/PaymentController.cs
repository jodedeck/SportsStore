using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Payment.Models;

namespace SportsStore.Payment.Controllers
{
    public class PaymentController : Controller
    {
        private string successUrl = "http://localhost:50036/Cart/SuccessPayment";
        private string failureUrl = "http://localhost:50036/Cart/FailPayment";

        public IActionResult Create()
        {
            return View(new PaymentDetails());
        }

        [HttpPost]
        public RedirectResult Create(PaymentDetails payementDetails)
        {
            //ajouter une loguique de réussite ou de rejet du payment
            //manipuler le paymentDetails pour voir si ca marche
            bool result = Validate(payementDetails);

            //Si le payment réussi, alors on renvoi vers l'écran de réussite
            if (result)
            {
                return Redirect(successUrl);
            }
            else
                return Redirect(failureUrl);
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

            //vérifier que le numéro de la carte est OK
            //apeller la validation jQuery


            return true;

        }
    }
}