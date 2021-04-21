using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServerApp.DTO;
using ServerApp.Entities;

namespace ServerApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private UserManager<User> _userManager;
        public UserController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDTO model){

            var user = new User{
                UserName = model.UserName,
                Email= model.Email,
                Name = model.Name,
                Gender = model.Gender,
                Created = DateTime.Now
            };
            var result = await _userManager.CreateAsync(user,model.Password);
            if(result.Succeeded){
                return Accepted(201);
            }
            return BadRequest(result.Errors);

        }
        
    }
}