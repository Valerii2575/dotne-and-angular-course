using DatingAppCourse.Api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingAppCourse.Api.Controllers
{
    
    public class AdminController(UserManager<AppUser> userManager) : BaseApiController
    {
        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("users-with-roles")]
        public async Task<ActionResult> GetUserWithRoles()
        {
            var users = await userManager.Users
                .OrderBy(u => u.UserName)
                .Select(u => new
                {
                    u.Id,
                    u.UserName,
                    Roles = userManager.GetRolesAsync(u).Result // Get roles synchronously for simplicity
                })
                .ToListAsync();

            return Ok(users);
        }

        //[Authorize(Policy = "RequireModeratorRole")]
        //[HttpGet]
        //public async Task<ActionResult> GetForModeration()
        //{
        //    return Ok("Only moderator can see this");
        //}

        //[Authorize(Policy = "RequireMemberRole")]
        //[HttpGet]
        //public async Task<ActionResult> GetForMember()
        //{
        //    return Ok("Only member can see this");
        //}

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("edit-roles/{username}")]
        public async Task<ActionResult> EditRoles(string username, [FromQuery] string roles)
        {
            var user = await userManager.FindByNameAsync(username);
            if (user == null)
                return NotFound("User not found");

            var roleList = roles.Split(",").ToList();
            var currentRoles = await userManager.GetRolesAsync(user);
            var result = await userManager.AddToRolesAsync(user, roleList.Except(currentRoles));
            if (!result.Succeeded)
                return BadRequest("Failed to add roles");

            result = await userManager.RemoveFromRolesAsync(user, currentRoles.Except(roleList));
            if (!result.Succeeded)
                return BadRequest("Failed to remove roles");

            return Ok(await userManager.GetRolesAsync(user));
        }
    }
}
