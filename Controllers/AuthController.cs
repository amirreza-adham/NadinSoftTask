using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NadinSoftTask.Domain.Entities;


namespace NadinSoftTask.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;


        public AuthController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var user = new AppUser
            {
                UserName = dto.UserName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Address = dto.Address
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok();
        }

        
    }

    // DTOها
    public record RegisterDto(string UserName, string Email, string PhoneNumber, string Address, string Password);
 

}

