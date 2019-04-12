using Api.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Data.Abstract
{
    public interface ICardRepository
    {
        Card GetById(int id);
        void SaveCard(Card order);
        int Commit();

        Card GetByCredentials(string lastName, string ccv, string cardNumer);
    }
}
