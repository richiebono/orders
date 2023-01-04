using Bono.Orders.Application.Interfaces;
using Bono.Orders.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bono.Orders.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly IOrderService OrderService;

        public OrderController(IOrderService OrderService)
        {
            this.OrderService = OrderService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            Console.WriteLine("Get");
            return Ok(this.OrderService.GetAll());
        }

        [HttpGet("GetAll/{userId}")]
        public IActionResult GetAll(string userId)
        {
            return Ok(this.OrderService.GetAll(userId));
        }

        [HttpPost]
        public IActionResult Post(OrderViewModel OrderViewModelViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(this.OrderService.Post(OrderViewModelViewModel));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            return Ok(this.OrderService.GetById(id));
        }

        [HttpPut]
        public IActionResult Put(OrderViewModel OrderViewModelViewModel)
        {
            return Ok(this.OrderService.Put(OrderViewModelViewModel));
        }

        [HttpDelete]
        public IActionResult Delete(string OrderViewModelId)
        {
            return Ok(this.OrderService.Delete(OrderViewModelId));
        }

        
    }
}