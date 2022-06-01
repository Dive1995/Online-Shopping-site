using System;
using System.Threading.Tasks;
using BusinessLogicLayer.Models;
using BusinessLogicLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingWebAPI.Controllers
{
    [ApiController]
    [Route("api/mail")]
    public class MailController : ControllerBase
    {
        private readonly IMailService _mailService;

        public MailController(IMailService mailService)
        {
            _mailService = mailService;
        }

        //[HttpPost("orderSuccess")]
        //public async Task<IActionResult> SendOrderSuccessMail([FromBody] MailRequest request)
        //{
        //    try
        //    {
        //        await _mailService.SendOrderEmailAsync(request);
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }

        //}

    }
}
