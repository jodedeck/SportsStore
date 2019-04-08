using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Infrastructure.Abstract;
using SportsStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository repository;
        private IOrderProcessor orderProcessor;
        private IRequestService requestService;
        private string paymentUrl = System.Configuration.ConfigurationManager.AppSettings["paymentUrl"];

        public CartController(IProductRepository repo, IOrderProcessor proc, IRequestService reqservice)
        {
            orderProcessor = proc;
            repository = repo;
            requestService = reqservice;

        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToRouteResult AddToCart(Cart cart, int productId, string returnUrl)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productId);

            if(product != null)
            {
                cart.Additem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string returnUrl)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productId);

            if(product != null)
            {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }

        public ViewResult SuccessPayment(Cart cart)
        {
            return View("Completed", cart);
        }

        public ViewResult FailPayment(Cart cart)
        {
            return View("FailPayment", cart);
        }

        [HttpPost]
        public RedirectResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }

            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, shippingDetails);
                //cart.Clear();
                //Je dois créer la requête au projet CORE
                //requestService.Send(cart);
                if (!string.IsNullOrEmpty(paymentUrl))
                {
                    return Redirect(paymentUrl);
                }


                //Process le retour du projet Core (payment réussi ou NON) et afficher une page en fonction du résultat
                //Si payement raté

                //Si payement réussi
                return null;
                //return View("Completed");
            }
            else
            {
                return null;
                //return View(shippingDetails);
            }
        }
    }
}