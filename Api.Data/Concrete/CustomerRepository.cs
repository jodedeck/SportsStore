﻿using Api.Data.Abstract;
using Api.Data.DataContext;
using Api.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Api.Data.Concrete
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApiDbContext db;

        public CustomerRepository(ApiDbContext db)
        {
            this.db = db;
        }
        public int Commit()
        {
            return db.SaveChanges();
        }

        public Customer GetById(int id)
        {
            return db.Customers.FirstOrDefault(x => x.Id == id);
        }

        public void SaveCustomer(Customer customer)
        {
            db.Customers.Add(customer);
        }
    }
}
