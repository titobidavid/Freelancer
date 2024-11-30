using FreeLancer.Models.Models.Domain;
using FreeLancer.Models.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FreeLancer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public readonly UserManager<Auth> _userManager;
        public readonly SignInManager<Auth> _signInManager;
        public readonly IConfiguration _config;
        private readonly ILogger<AuthController> _logger;

        public AuthController(UserManager<Auth> userManager, SignInManager<Auth> signInManager, IConfiguration config, ILogger<AuthController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            _logger = logger;
        }
        //[HttpPost]
        //public async Task<IActionResult> Register(Auth user)
        //{
        //    return Ok();
        //}

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);
            var tuser = (Freelancer)user;
            if (user != null)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);
                if(result.Succeeded)
                {
                    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                    var Sectoken = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Issuer"],expires: DateTime.Now.AddMinutes(120),signingCredentials: credentials,
                        claims: new List<Claim>(
                        new Claim[]
                        {
                            new("role", Convert.ToString(tuser.role))
                        }
                    ));

                    var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);

                    return Ok(token);
                }
                _logger.LogInformation(string.Format("email or password is wrong"));
            }
            return StatusCode(StatusCodes.Status401Unauthorized);
        }
    }
}
