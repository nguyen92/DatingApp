
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.DTOS;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API.Controllers
{
   [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;

        public AuthController(IAuthRepository repo, IConfiguration config){
            _repo = repo;
            _config = config;
        } 
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterForDto userRegisterForDto)
        {
            userRegisterForDto.Username = userRegisterForDto.Username.ToLower();
            if (await _repo.UserExists(userRegisterForDto.Username))
            {
                return BadRequest("user already exists");
            }
            var useToCreate = new User {
                Username = userRegisterForDto.Username
            };

            var createdUser = await _repo.Register(useToCreate, userRegisterForDto.Password);
            return StatusCode(201);

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login (UserLoginForDto userLoginForDto)
        {
          var userFromRepo = await _repo.Login(userLoginForDto.Username.ToLower(), userLoginForDto.Password);
          if ( userFromRepo == null)
                return Unauthorized();

            var claims = new []
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.ID.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.Username)
                
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                     .GetBytes(_config.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                 Expires = DateTime.Now.AddDays(1),
                 SigningCredentials = creds   
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Ok(new{
                token = tokenHandler.WriteToken(token)
            });
        }
    }
}