using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LKDUS_API.Contracts;
using LKDUS_API.Data;
using LKDUS_API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
 

namespace LKDUS_API.Controllers
{
    /// <summary>
    /// Endpoint used to interact with the measurements in the LKDUS database
    /// </summary>
    [Route("api/[controller]")]
    [Route("api/login")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]

    public class AspNetUsersController : ControllerBase
    {
        //    private UserManager<AspUserDTO> userManager1;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly ILoggerService logger;
        private readonly IConfiguration config;
        private readonly IMapper mapper;
        // private readonly IAspUserRepository aspNetUserRepository;

        public AspNetUsersController(
        //    UserManager<AspUserDTO> userManager1,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            ILoggerService logger,
            IConfiguration config,
            IMapper mapper
            //,
            // IAspUserRepository aspNetUserRepository
            )
        {
            // this.userManager1 = userManager1;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.logger = logger;
            this.config = config;
            this.mapper = mapper;
            // this.aspNetUserRepository = aspNetUserRepository;
        }

        /// <summary>
        /// User Login endpoint
        /// </summary>
        /// <param name="aspNetUserDTO"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] AspUserDTO aspNetUserDTO)
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
                    return Ok(new { token = tokenString });
                }




                this.logger.LogInfo($"{location}: {userName} Not authenticated");

                return Unauthorized(aspNetUserDTO);
            }
            catch (Exception e)
            {

                return InternalError($"{location}:{e.Message} - {e.InnerException}");

            }







        }


      

        /// <summary>
        /// Get user by id 
        /// </summary>
        /// <param name="id"></param>

        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUser(string id)
        {
            var location = GetControllerActionNames();
            try
            {
                this.logger.LogInfo($"{location}: Attempted Call for id: {id}");

                //IList<AspUserDTO> operetorList = new List<AspUserDTO>();
                var users = this.userManager.Users;
                AspUserDTO usr = null;

                foreach (var uuser in users)
                {
                    if (uuser.Id == id)
                    {
                        usr = new AspUserDTO()
                        {
                            UserName = uuser.UserName,
                            Id = uuser.Id,
                            Password = uuser.PasswordHash,




                        };


                    }
                }
                //   AspUserDTO user = await userManager.FindByIdAsync(id);
                if (usr == null)
                {
                    this.logger.LogWarn($"{location}: Failed to retrieve record with id: {id}");
                    return NotFound();
                }


                var response = this.mapper.Map<AspUserDTO>(usr);
                this.logger.LogInfo($"{location}: Successfully got record with id: {id}");
                return Ok(response);
            }
            catch (Exception e)
            {
                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
        }  
          
        
        /// <summary>
            /// Get all Users
            /// 
            /// </summary>
            /// <returns>List of all Users</returns>
            [HttpGet] 
           [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public   IActionResult GetUsers( )
        {
            var location = GetControllerActionNames();
            try
            {


                // _logger.LogInfo($"{location}: Attempted Get All Users");
                var users = this.userManager.Users;

                IList<AspUserDTO> operetorList = new List<AspUserDTO>();

                foreach (var u in users)
                {
                    var operatorUser = new AspUserDTO
                    {
                        Id = u.Id,
                        UserName = u.UserName,
                        Password = u.PasswordHash
                    };

                    operetorList.Add(operatorUser);


                }


                var response = operetorList;
                //this.mapper.Map<IList<AspNetUserDTO>>(users);
                // _logger.LogInfo($"{location}: Sucessfully got all Users");

                return Ok(response);
            }
            catch (Exception e)
            {
                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }


        }


      /// <summary>
        /// Creates a asp user 
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        [HttpPut]
      //  [Authorize(Roles = "Admin, Master")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] AspUserCreateDTO userDTO)
        {
            var location = GetControllerActionNames();
            try
            {
                this.logger.LogInfo($"{location}: User creation  Attempted");
                if (await userManager.FindByNameAsync(userDTO.UserName) == null)
                {

                    var user = new IdentityUser
                    {
                        UserName = userDTO.UserName,
                        Email = userDTO.UserName+"@finieris.lv",

                    };

                    var result = await userManager.CreateAsync(user, "1111");
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, "Operator");
                        this.logger.LogInfo($"{location}: User creation was created");
                        this.logger.LogInfo($"{location}: {user}");
                        return Created("Create", new { user });
                    } 


                    
                }
                return BadRequest(ModelState);


          

           
               
                

                
 
            }
            catch (Exception e)
            {
                return InternalError($"{location}: {e.Message} - {e.InnerException}");

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



        /// <summary>
        /// Updates a asp user 
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        //  [Authorize(Roles = "Admin, Master")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update( [FromBody] AspUserDTO userDTO, string id)
        {
            var location = GetControllerActionNames();
            try
            {
                this.logger.LogWarn($"{location}: user update atempted - id: {id}");
                if (  string.IsNullOrEmpty(id) || string.IsNullOrEmpty(userDTO.Id))
                {
                    this.logger.LogWarn($"{location}: user  update failed with wrong data");
                    return BadRequest(ModelState);
                }

                var user = await userManager.FindByIdAsync(id);// .machineRepository.isExists(id);

                if (user == null)
                {
                    this.logger.LogWarn($"{location}: user Data was not found");
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    this.logger.LogWarn($"{location}: pack Data was Incomplete");
                    return BadRequest(ModelState);

                }

                user.UserName = userDTO.UserName;
                var isGood = await this.userManager.UpdateAsync(user);
              //  var isGood = await this.machineRepository.Update(machine);
                if (!isGood.Succeeded)
                {

                    return InternalError($"{location}: user update failed");
                }

                this.logger.LogInfo($"{location}: user Data with id: {id} was updated");
                return NoContent();

            }
            catch (Exception e)
            {
                return InternalError($"{location}: {e.Message} - {e.InnerException}");

            }

        }


        /// <summary>
        /// Removes USER by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(string id)
        {

            var location = GetControllerActionNames();
            try
            {
                this.logger.LogWarn($"{location}: user deletion atempted - id: {id}");
                if (string.IsNullOrEmpty(id))
                {
                    this.logger.LogWarn($"{location}: user deleting failed with wrong data");
                    return BadRequest();
                }
                var user = await userManager.FindByIdAsync(id);// .machineRepository.isExists(id);
              //  var isExist = await this. machineRepository.isExists(id);

                if (user == null)
                {
                    this.logger.LogWarn($"{location}: user Data with id: {id} was not found");
                    return NotFound();
                }
                
                var isGood = await this.userManager.DeleteAsync(user);

                if ( !isGood.Succeeded)
                {
                    return InternalError($"{location}: user Delete failed");
                }

                this.logger.LogWarn($"{location}:  user Data with id: {id} was deleted");
                return NoContent();

            }
            catch (Exception e)
            {
                return InternalError($"{location}: {e.Message} - {e.InnerException}");

            }

        }



    }


}
