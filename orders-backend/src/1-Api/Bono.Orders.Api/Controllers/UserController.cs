using Bono.Orders.Application.Interfaces;
using Bono.Orders.Application.ViewModels;
using Bono.Orders.Infrastructure.Auth.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bono.Orders.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]    
    public class UsersController : ControllerBase
    {

        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(this.userService.GetAll());
        }

        [AllowAnonymous]
        [HttpPost]        
        public IActionResult Post(UserViewModel userViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(this.userService.Post(userViewModel));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            return Ok(this.userService.GetById(id));
        }

        [HttpPut]
        public IActionResult Put(UserViewModel userViewModel)
        {
            return Ok(this.userService.Put(userViewModel));
        }

        [HttpDelete]
        public IActionResult Delete(string userId)
        {
            return Ok(this.userService.Delete(userId));
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate(UserAuthenticateRequestViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = this.userService.Authenticate(userViewModel);

                if (result.IsValid)
                {
                    return Ok(result.Entity);                   
                }
                return Unauthorized(result); //401
            }
            return BadRequest(); //400            
        }
    }
}