using Bono.Orders.Api.Filters;
using Bono.Orders.Application.Interfaces;
using Bono.Orders.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;

namespace Bono.Orders.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class OrderController : BaseController
    {

        private readonly IOrderService OrderService;

        public OrderController(IOrderService OrderService)
        {
            this.OrderService = OrderService;
        }

        [HttpGet]
        [ResponseHeader("Access-Control-Expose-Headers", "Content-Range")]
        public IActionResult Get([FromQuery] FilterViewModel filterRequest)
        {
            var orders = this.OrderService.Filter(filterRequest);
            return OkFilter(orders, "orders", filterRequest.start, filterRequest.size, this.OrderService.Count(filterRequest));
        }

        [HttpGet("GetAll/{userId}")]
        public IActionResult GetAll(string userId)
        {
            return Ok(this.OrderService.GetAll(userId));
        }

        [HttpPost]
        public IActionResult Post(OrderViewModel OrderViewModelViewModel)
        {
            try
            {
                OrderViewModelViewModel.UserId = UserId();
                var result = this.OrderService.Post(OrderViewModelViewModel);

                if (result.Errors.Any())
                    return BadRequest(result);

                return Ok(this.OrderService.Post(OrderViewModelViewModel).Data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            return Ok(this.OrderService.GetById(id));
        }

        [HttpPut("{id}")]
        public IActionResult Put(string id, OrderViewModel OrderViewModelViewModel)
        {
            try
            {
                //OrderViewModelViewModel.UserId = UserId();
                var result = this.OrderService.Put(OrderViewModelViewModel);

                if (result.Errors.Any())
                    return BadRequest(result);

                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            return Ok(this.OrderService.Delete(id));
        }

    }
}