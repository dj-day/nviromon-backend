using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System;
using Microsoft.IdentityModel.Tokens;
using Nviromon.Data;
using Nviromon.Models;
using Nviromon.Request_Models;
using Microsoft.Extensions.Configuration;


namespace Nviromon.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository repository;

        private readonly IConfiguration configuration;
        public AuthController(IAuthRepository repository, IConfiguration configuration)
        {
            this.repository = repository;
            this.configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> login(LoginRequestModel user)
        {
            var userFromRepository = await repository.login(user.username, user.password);

            if(userFromRepository == null)
            {
                return BadRequest("Username or password is incorrect");
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepository.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepository.username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes((configuration.GetSection("AppSettings:Token").Value)));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok( new {
                token = tokenHandler.WriteToken(token)
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> register(CreateUserRequestModel user)
        {

            if( await repository.userExists(user.username))
            {
                return BadRequest("Username is taken.");
            }

            var userToCreate = new User
            {
                username = user.username.ToLower(),
                firstName = user.firstName,
                lastName = user.lastName,
            };

            var createdUser = await repository.register(userToCreate, user.password);
            
            return Ok("Created");
        }
    }
}