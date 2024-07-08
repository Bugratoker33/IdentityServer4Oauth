using IdentityServer.Dtos;
using IdentityServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static IdentityServer4.IdentityServerConstants;

namespace IdentityServer.Controllers
{
    [Authorize(LocalApi.PolicyName)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody]SingupDtos  signupDto)
        {
            var user = new ApplicationUser
            {
                UserName = signupDto.UserName,
                Email = signupDto.Email,
                City = signupDto.City
            };

            var result = await _userManager.CreateAsync(user, signupDto.Password);

            if (result.Succeeded)
            {
                return NoContent();
            }
          

            return BadRequest(result.Errors);
        }
    }


    // [HttpGet]
    //public async Task<IActionResult> GetUser()
    //{
    //    var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);

    //    if (userIdClaim == null) return BadRequest();

    //    var user = await _userManager.FindByIdAsync(userIdClaim.Value);

    //    if (user == null) return BadRequest();

    //    return Ok(new
    //    {
    //        user.Id,
    //        user.UserName,
    //        user.Email,
    //        user.City,
    //    });

    //}
}

