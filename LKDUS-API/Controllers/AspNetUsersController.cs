using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using LKDUS_API.Contracts;
using LKDUS_API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace LKDUS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AspNetUsersController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly ILoggerService logger;
        private readonly IConfiguration config;

        public AspNetUsersController(SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            ILoggerService logger,
            IConfiguration config)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.logger = logger;
            this.config = config;
        }

        /// <summary>
        /// User Login endpoint
        /// </summary>
        /// <param name="aspNetUserDTO"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] AspNetUserDTO aspNetUserDTO)
        {

            var location = GetControllerActionNames();
            try
            {
                var userName = aspNetUserDTO.UserName;
                //var password = aspNetUserDTO.Password;
                var password = aspNetUserDTO.Password;// aspNetUserDTO.Password;
                this.logger.LogInfo($"{location}: Login attempted call from user {userName}");

              // var user1 = this.userManager.FindByNameAsync(userName);

               //var res = this.signInManager.UserManager.CheckPasswordAsync(user1.Id, password);
                
                var result = await signInManager.PasswordSignInAsync(userName.ToString(), "1111", false, false);

               // signInManager.UserManager.CheckPasswordAsync(aspNetUserDTO, aspNetUserDTO.Password);

                if (result.Succeeded)
                {
                    var user = await this.userManager.FindByNameAsync(userName);
                    this.logger.LogInfo($"{location}: {userName} Sucessfully Authenticated");
                    var tokenString = await GenerateJSONWebToken(user);
                    return Ok(new {  token = tokenString});
                }
                
                this.logger.LogInfo($"{location}: {userName} Not authenticated");

                return Unauthorized(aspNetUserDTO);
            }
            catch (Exception e)
            {

                return InternalError($"{location}:{e.Message} - {e.InnerException}");

            }



           

            

        }

        private async Task<string> GenerateJSONWebToken(IdentityUser user)
        {
            //

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {

                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),


            };

            var roles = await this.userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(r => new Claim(ClaimsIdentity.DefaultRoleClaimType, r)));

            var token = new JwtSecurityToken(
                  this.config["Jwt:Issuer"]
                , this.config["Jwt:Issuer"]
                , claims
                , null
                , expires: DateTime.Now.AddMinutes(5)
                , signingCredentials: credentials
                );


            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GetControllerActionNames()
        {
            var controller = ControllerContext.ActionDescriptor.ControllerName;
            var action = ControllerContext.ActionDescriptor.ActionName;
            return $"{controller}-{action}";
        }

        private ObjectResult InternalError(string message)
        {
            this.logger.LogError(message);
            return StatusCode(500, "Something is broken, please contact your supervisor");
        }


    }


}
