using Bono.Orders.Api.Filters;
using Bono.Orders.Application.Interfaces;
using Bono.Orders.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Bono.Orders.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class OrderTypeController : BaseController
    {

        private readonly IOrderTypeService OrderTypeService;

        public OrderTypeController(IOrderTypeService OrderTypeService)
        {
            this.OrderTypeService = OrderTypeService;
        }

        [HttpGet]
        [ResponseHeader("Access-Control-Expose-Headers", "Content-Range")]
        public IActionResult Get([FromQuery] FilterViewModel filterRequest)
        {
            var ordersType = this.OrderTypeService.Filter(filterRequest);
            return OkFilter(ordersType, "orderTypes", filterRequest.start, filterRequest.size, this.OrderTypeService.Count(filterRequest));
        }        
    }
}