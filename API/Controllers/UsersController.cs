using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace API.Controllers
{
    // [Route("api/[controller]")]
    // [ApiController]
    public class UsersController(DataContext context) : BaseApiController
    {
       // private readonly DataContext context = context;  No need becouse of primary costructor.
        [AllowAnonymous]
        [HttpGet]
        public async Task< ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            var users= await context.Users.ToListAsync();
            
           // return Ok(users);
            return users;


        }
            
         [Authorize]    
         [HttpGet("{id:int}")]
        public async Task< ActionResult<AppUser>> GetUser(int id)
        {
            var user=await context.Users.FindAsync(id);
            if(user==null)
            return NotFound();
        
        return user;

        }
    }
}
