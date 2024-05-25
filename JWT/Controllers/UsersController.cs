using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IHttpContextAccessor _contextAccessor;

        public UsersController(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Check()
        {
            return Ok("it is working ");
        }

        [HttpPost("example")]
        [Authorize]
        public async Task<IActionResult> Calculate([FromQuery]int a, [FromQuery]int b)
        {
            var c = a + b;
            return Ok(c);
        }

        [HttpGet("token")]
        public string GetToken()
        {
            var signinkey = System.Text.Encoding.UTF32
        .GetBytes("weyuyweuguyfdsgfuyds82357428726445326shbcdfsjyueuysefg");

            var claim = new List<Claim>()
            {
                new Claim(ClaimTypes.GivenName,"Husan"),
                new Claim(ClaimTypes.NameIdentifier,"sdhkjd1234"),
                new  Claim(ClaimTypes.Role,"SuperAdmin")
            };

            var securty  = new JwtSecurityToken(issuer: "Blog.API", audience: "Blog.Clint", claims:claim,
                expires:DateTime.Now.AddMinutes(10),signingCredentials:new SigningCredentials(
                    new SymmetricSecurityKey(signinkey), algorithm: "HS256"));

            var token = new JwtSecurityTokenHandler().WriteToken(securty);
            return token;
        }

        [HttpGet("Profile")]
        [Authorize]
        public async Task<IActionResult> GetProfile()
        {
            var givenname = _contextAccessor.HttpContext!.User.FindFirstValue(claimType: ClaimTypes.GivenName);
            var nameIdentifier = _contextAccessor.HttpContext!.User.FindFirstValue(claimType: ClaimTypes.NameIdentifier);
            var role = _contextAccessor.HttpContext!.User.FindFirstValue(claimType: ClaimTypes.Role);

            return Ok(new {givenname,nameIdentifier,role });
        }
    }
}

