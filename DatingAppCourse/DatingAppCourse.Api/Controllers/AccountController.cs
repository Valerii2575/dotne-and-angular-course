using DatingAppCourse.Api.Data;
using DatingAppCourse.Api.DTOs;
using DatingAppCourse.Api.Entities;
using DatingAppCourse.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;
using System.Security.Cryptography;
using System.Text;

namespace DatingAppCourse.Api.Controllers
{
    public class AccountController(DataContext context, ITokenService tokenService) : BaseApiController
    {
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            // Validate the incoming data
            if (registerDto == null)
            {
                return BadRequest("Invalid registration data");
            }
            if (string.IsNullOrWhiteSpace(registerDto.UserName) || string.IsNullOrWhiteSpace(registerDto.Password))
            {
                return BadRequest("Username and password are required");
            }
            if (registerDto.Password != registerDto.ConfirmPassword)
            {
                return BadRequest("Passwords do not match");
            }
            // Check if the user already exists
            if (await UserExist(registerDto.UserName))
            {
                return BadRequest("Username already exists");
            }
            // Ensure the password meets security requirements
            if (registerDto.Password.Length < 6)
            {
                return BadRequest("Password must be at least 6 characters long");
            }
            if (!registerDto.Password.Any(char.IsUpper) || !registerDto.Password.Any(char.IsLower) || !registerDto.Password.Any(char.IsDigit))
            {
                return BadRequest("Password must contain at least one uppercase letter, one lowercase letter, and one digit");
            }
            if (registerDto.Password.Any(char.IsWhiteSpace))
            {
                return BadRequest("Password cannot contain whitespace");
            }
            if (registerDto.UserName.Length < 3 || registerDto.UserName.Length > 20)
            {
                return BadRequest("Username must be between 3 and 20 characters long");
            }
            if (!registerDto.UserName.All(char.IsLetterOrDigit))
            {
                return BadRequest("Username can only contain letters and digits");
            }

            return Ok();
            // Registration logic goes here
            //using var hmac = new HMACSHA512();

            //var user = new AppUser
            //{
            //    UserName = registerDto.UserName,
            //    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            //    PasswordSalt = hmac.Key
            //};

            //await context.Users.AddAsync(user);
            //await context.SaveChangesAsync();

            //var userDto = new UserDto
            //{
            //    UserName = user.UserName,
            //    Token = tokenService.CreateToken(user)
            //};

            //return Ok(userDto);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            // Validate the incoming data
            if (loginDto == null)
            {
                return BadRequest("Invalid login data");
            }
            if (string.IsNullOrWhiteSpace(loginDto.UserName) || string.IsNullOrWhiteSpace(loginDto.Password))
            {
                return BadRequest("Username and password are required");
            }
            // Check if the user exists
            var user = await context.Users.SingleOrDefaultAsync(x => x.UserName.ToLower() == loginDto.UserName.ToLower());
            if (user == null)
            {
                return Unauthorized("Invalid username or password");
            }
            // Verify the password
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                {
                    return Unauthorized("Invalid username or password");
                }
            }

            var userDto = new UserDto
            {
                UserName = user.UserName,
                Token = tokenService.CreateToken(user)
            };

            return Ok(userDto);
        }

        private async Task<bool> UserExist(string userName)
        {
            return await context.Users.AnyAsync(x => x.UserName.ToLower() == userName.ToLower());
        }
    }
}
