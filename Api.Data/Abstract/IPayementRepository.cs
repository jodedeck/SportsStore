using Api.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Data.Abstract
{
    public interface IPaymentRepository
    {
        PaymentData GetById(int id);
        void SavePayment(PaymentData paymentData);
        int Commit();
    }
}
