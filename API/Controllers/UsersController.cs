using System.Security.Claims;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace API.Controllers
{
    // [Route("api/[controller]")]
    // [ApiController]
    [Authorize]
    public class UsersController(IUserRepository userRepository,IMapper mapper) : BaseApiController
    {
       // private readonly DataContext context = context;  No need becouse of primary costructor.
        
        [HttpGet]
        public async Task< ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            var users= await userRepository.GetMembersAsync();

            //var usersToReturn=mapper.Map<IEnumerable<MemberDto>>(users);
            
           // return Ok(usersToReturn);
           return Ok(users);
         
        }
            
         
         [HttpGet("{username}")]
        public async Task< ActionResult<MemberDto>> GetUser(string  username)
        {
            var user=await userRepository.GetMemberAsync(username);
        
             if(user==null) return NotFound();
       
             //return mapper.Map<MemberDto>(user);

             return user;

        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            var username=User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if(username==null) return BadRequest("No yourname found in token");

            var user=await userRepository.GetUserByUsernameAsync(username);

            if(user==null) return BadRequest("could not find user");

            mapper.Map(memberUpdateDto,user);
             
             userRepository.Update(user);

            if(await userRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to update the user");
        }
    }
}
