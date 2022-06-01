using System;
using System.Threading.Tasks;
using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Services
{
    public interface IMailService
    {
        Task SendOrderEmailAsync(string receiverEmail, OrderDto order);
    }
}
