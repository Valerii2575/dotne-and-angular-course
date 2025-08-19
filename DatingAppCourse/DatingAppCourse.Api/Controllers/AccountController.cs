using DatingAppCourse.Api.Data;
using DatingAppCourse.Api.DTOs;
using DatingAppCourse.Api.Entities;
using DatingAppCourse.Api.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingAppCourse.Api.Controllers
{
    public class AccountController(UserManager<AppUser> userManager, ITokenService tokenService) : BaseApiController
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

            var user = new AppUser
            {
                KnownAs = registerDto.UserName,
                Gender = "",
                UserName = registerDto.UserName,

            };

            var result = await userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.Select(e => e.Description).ToList());
            }

            var userDto = new UserDto
            {
                UserName = user.UserName,
                Token = await tokenService.CreateToken(user),
                TokenExpiration = DateTime.UtcNow.AddDays(7) // Set token expiration to 7 days
            };

            return Ok(userDto);
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
            var user = await userManager.Users.FirstOrDefaultAsync(x => x.NormalizedUserName == loginDto.UserName.ToUpper());
            if (user == null)
            {
                return Unauthorized("Invalid username or password");
            }

            var result = await userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!result)
            {
                return Unauthorized("Invalid username or password");
            }


            var userDto = new UserDto
            {
                UserName = user.UserName,
                Token = await tokenService.CreateToken(user),
                TokenExpiration = DateTime.UtcNow.AddDays(7)
            };

            return Ok(userDto);
        }

        private async Task<bool> UserExist(string userName)
        {
            return await userManager.Users.AnyAsync(x => x.NormalizedUserName == userName.ToUpper());
        }
    }
}
