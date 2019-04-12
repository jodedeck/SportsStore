using Api.Data.Abstract;
using Api.Data.DataContext;
using Api.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Api.Data.Concrete
{
    public class CardRepository : ICardRepository
 {
        private readonly ApiDbContext db;

        public CardRepository(ApiDbContext db)
        {
            this.db = db;
        }
        public int Commit()
        {
            return db.SaveChanges();
        }

        public Card GetByCredentials(string name, string ccv, string cardNumer)
        {
            return db.Cards.FirstOrDefault(x => x.cardholderName == name && x.cvv == ccv && x.cardNumber == cardNumer);
        }

        public Card GetById(int id)
        {
            return db.Cards.FirstOrDefault(x => x.Id == id);
        }

        public void SaveCard(Card card)
        {
            db.Cards.Add(card);
        }
    }
}
