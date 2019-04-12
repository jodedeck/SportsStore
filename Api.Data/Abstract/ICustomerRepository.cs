using Api.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Data.Abstract
{
    public interface ICustomerRepository
    {
        Customer GetById(int id);
        void SaveCustomer(Customer order);
        int Commit();
    }
}
