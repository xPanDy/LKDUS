using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LKDUS_API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LKDUS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AspNetUsersController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;


        public AspNetUsersController(SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
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
            var userName = aspNetUserDTO.UserName;
            //var password = aspNetUserDTO.Password;
            var password = "";
            var result = await signInManager.PasswordSignInAsync(userName, password, false, false );


            if (result.Succeeded)
            {
                var user = await this.userManager.FindByNameAsync(userName);
                return Ok(user);
            }

            return Unauthorized(aspNetUserDTO);

        }
    }
}
