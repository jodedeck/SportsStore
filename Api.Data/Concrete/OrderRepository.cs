using Api.Data.Abstract;
using Api.Data.DataContext;
using Api.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Api.Data.Concrete
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApiDbContext db;

        public OrderRepository(ApiDbContext db)
        {
            this.db = db;
        }

        public int Commit()
        {
            return db.SaveChanges();
        }

        public Order GetById(int id)
        {
            return db.Orders.FirstOrDefault(x => x.Id == id);
        }

        public void SaveOrder(Order order)
        {
            if(order != null)
            {
                db.Orders.Add(order);
            }
        }
    }
}
