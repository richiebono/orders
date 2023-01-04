using Bono.Orders.Application.Interfaces;
using Bono.Orders.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bono.Orders.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class OrderTypeController : ControllerBase
    {

        private readonly IOrderTypeService OrderTypeService;

        public OrderTypeController(IOrderTypeService OrderTypeService)
        {
            this.OrderTypeService = OrderTypeService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(this.OrderTypeService.GetAll());
        }        
    }
}