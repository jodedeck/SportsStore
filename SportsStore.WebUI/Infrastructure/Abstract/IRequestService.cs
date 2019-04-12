using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.WebUI.Infrastructure.Abstract
{
    public interface IRequestService
    {
        void Send(Cart cart);
        Task<HttpResponseMessage> GetTransactionId(decimal value);
    }
}
