using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Abstract;
using Api.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PaymentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IOrderRepository orderRepository;
        private List<string> transactions = new List<string>();


        public TransactionController(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
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

    }
}