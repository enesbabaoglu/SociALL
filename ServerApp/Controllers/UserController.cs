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
        private readonly SignInManager<User> _signInManager;

        public UserController(UserManager<User> userManager,SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
        [HttpPost("login")]
        public async Task<IActionResult> Login (LoginDTO model){

            var user = await _userManager.FindByNameAsync(model.UserName);

            if(user==null){
               return BadRequest(new {message = "Username is incorrect"});
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user,model.Password,false);

            if(result.Succeeded){

                return Ok(new { 
                    Token = "token",
                    UserName = model.UserName    
                });

            }
            return Unauthorized();

        }
        
    }
}