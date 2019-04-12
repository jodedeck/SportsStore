using Api.Data.Abstract;
using Api.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Data.Concrete
{
    public class CardRepository : ICardRepository
    {
        public int Commit()
        {
            throw new NotImplementedException();
        }

        public Order GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void SaveOrder(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
