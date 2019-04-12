using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Infrastructure.Abstract;
using SportsStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<RedirectResult> Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }

            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, shippingDetails);

                var response = await requestService.GetTransactionId(cart.ComputeTotalValue());
                string transactionId = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(paymentUrl))
                {
                    return Redirect(paymentUrl + "/" + transactionId);
                }

                return null;
            }
            else
            {
                return null;
                //return View(shippingDetails);
            }
        }
    }
}