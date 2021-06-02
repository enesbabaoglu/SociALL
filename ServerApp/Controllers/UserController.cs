using System.Security.Claims;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerApp.Repositories.Abstract;
using AutoMapper;
using ServerApp.DTO;
using ServerApp.Helpers;
using System;
using ServerApp.Entities;

namespace ServerApp.Controllers
{
    //[ServiceFilter(typeof(LastActiveActionFilter))]
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserToUserRepository _userToUserRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper, IUserToUserRepository userToUserRepository = null)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _userToUserRepository = userToUserRepository;
        }

        [HttpGet]
        public IActionResult GetUsers([FromQuery]UserQueryParams userQueryParams)
        {
            userQueryParams.UserId= int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var users = _userRepository.GetAllWithIncludes(x=>x.Id != userQueryParams.UserId, x => x.Images);
            var user= _userRepository.GetWithIncludes(x=>x.Id == userQueryParams.UserId, x => x.Images,x => x.Followers,x => x.Following);

            if(userQueryParams.Followers){  
                var list = user.Followers.Select(x=> x.FollowerId);
                users = users.Where(u => list.Contains(u.Id));
            }

            if(userQueryParams.Followings){  
                var list = user.Following.Select(x=> x.UserId);
                users = users.Where(u => list.Contains(u.Id));
            }
         
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

        [HttpPut("{id}")]
        public IActionResult Update(int id, UserForUpdateDTO dto)
        {

            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return BadRequest("Not Valid Request");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _userRepository.Get(x => x.Id == id);

            _mapper.Map(dto, user);

            _userRepository.Update(user);

            return Ok();
        }

        [HttpPost("{followerUserId}/follow/{userId}")]
        public IActionResult FollowUser(int followerUserId, int userId)
        {
            try
            {

                if (followerUserId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                    return Unauthorized();

                if (followerUserId == userId)
                    return BadRequest("Kendini mi takip etcen ? ");

                if (_userRepository.Get(x => x.Id == userId) == null)
                {
                    return BadRequest("Kullanıcı blunamadı");
                }

                var follow = new UserToUser()
                {
                    UserId = userId,
                    FollowerId = followerUserId
                };

                if (IsFollowedUser(followerUserId, userId))
                {
                    _userToUserRepository.Delete(follow);
                }
                else
                {
                    _userToUserRepository.Create(follow);

                }

                return Ok();

            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
                throw ex;
            }

        }

        [HttpGet("{followerUserId}/isFollowedUser/{userId}")]
        public bool IsFollowedUser(int followerUserId, int userId)
        {

            var userToUser = _userToUserRepository.GetAll(x => x.FollowerId == followerUserId && x.UserId == userId);

            return userToUser.Any();

        }
    }
}