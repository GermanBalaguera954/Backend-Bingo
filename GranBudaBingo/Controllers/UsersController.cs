using GranBudaBingo.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GranBudaBingo.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IConfiguration configuration;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;

        public UsersController(ApplicationDbContext context, IConfiguration configuration, SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager)
        {
            this.context = context;
            this.configuration = configuration;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        private async Task<Authentication> CreateToken(UserCredentials userCredentials)
        {
            var user = await userManager.FindByEmailAsync(userCredentials.Email);
            if (user == null)
            {
                return null;
            }

            var claims = new List<Claim>()
            {
                new Claim("email", userCredentials.Email),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["keyJwt"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddYears(1);

            var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims,
                expires: expiration, signingCredentials: creds);

            return new Authentication()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiration = expiration
            };
        }

        [HttpPost("register")]
        public async Task<ActionResult<Authentication>> Register(UserCredentials userCredentials)
        {
            var user = new IdentityUser { UserName = userCredentials.Email, Email = userCredentials.Email };
            var result = await userManager.CreateAsync(user, userCredentials.Password);

            if (result.Succeeded)
            {
                var token = await CreateToken(userCredentials);
                if (token != null)
                {
                    return Ok(token);
                }
                return BadRequest("No se pudo crear el token");
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<Authentication>> Login(UserCredentials userCredentials)
        {
            var result = await signInManager.PasswordSignInAsync(userCredentials.Email, userCredentials.Password,
                isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var token = await CreateToken(userCredentials);
                if (token != null)
                {
                    return Ok(token);
                }
                return BadRequest("No se pudo crear el token");
            }
            else
            {
                return BadRequest("Login Incorrecto");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserById(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var userDto = new UserDto
            {
                Email = user.Email,
            };

            return Ok(userDto);
        }
    }
}
