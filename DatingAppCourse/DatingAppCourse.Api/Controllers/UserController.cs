using AutoMapper;
using DatingAppCourse.Api.DTOs;
using DatingAppCourse.Api.Entities;
using DatingAppCourse.Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingAppCourse.Api.Controllers
{
    [Authorize]
    public class UserController(IUserRepository userRepository, IMapper mapper) : BaseApiController
    {
        [HttpGet]
        [Route("users")]
        public async Task<ActionResult<IEnumerable<MemberDto>>> Get()
        {
            var users = await userRepository.GetUsersAsync();
            var members = mapper.Map<IEnumerable<MemberDto>>(users);
            return Ok(members);
        }

        [HttpGet]
        [Route("users/{id:int}")]
        public async Task<ActionResult<MemberDto>> GetById(int id)
        {
            var user = await userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var member = mapper.Map<MemberDto>(user);
            return Ok(member);
        }

        [HttpGet]
        [Route("users/byname/{userName}")]
        public async Task<ActionResult<MemberDto>> GetByName(string userName)
        {
            var user = await userRepository.GetUserByUsernameAsync(userName);
            if (user == null)
            {
                return NotFound();
            }
            var member = mapper.Map<MemberDto>(user);
            return Ok(member);
        }
    }
}
