using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerApp.Repositories.Abstract;
using AutoMapper;
using ServerApp.DTO;

namespace ServerApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _userRepository.GetAllWithIncludes(null, x => x.Images).ToList();

            var result = _mapper.Map<IEnumerable<UserForListDTO>>(users);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _userRepository.GetWithIncludes(x => x.Id == id, x => x.Images);
            var result = _mapper.Map<UserForDetailsDTO>(user);

            return Ok(result);
        }
    }
}