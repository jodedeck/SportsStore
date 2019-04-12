using Api.Data.Abstract;
using Api.Data.DataContext;
using Api.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Api.Data.Concrete
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApiDbContext db;

        public PaymentRepository(ApiDbContext db)
        {
            this.db = db;
        }
        public int Commit()
        {
            return db.SaveChanges();
        }

        public PaymentData GetById(int id)
        {
            return db.Payments.FirstOrDefault(x => x.Id == id);
        }

        public void SavePayment(PaymentData paymentData)
        {
            db.Payments.Add(paymentData);
        }
    }
}
