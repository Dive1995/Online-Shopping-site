using System;
using BusinessLogicLayer.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingWebAPI.Controllers
{
    [ApiController]
    [Route("api/delivery")]
    public class DeliveryOptionsController : ControllerBase
    {
        private readonly IDeliveryBLL _deliveryBLL;

        public DeliveryOptionsController(IDeliveryBLL deliveryBLL)
        {
            _deliveryBLL = deliveryBLL;
        }

        [HttpGet]
        public ActionResult GetDeliveryOptions()
        {
            var options = _deliveryBLL.GetDeliveryOptions();
            return Ok(options);
        }
    }
}
