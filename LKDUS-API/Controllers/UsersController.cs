using System;
using System.Collections.Generic;
 
using System.Threading.Tasks;
using AutoMapper;
using LKDUS_API.Contracts;
using LKDUS_API.Data;
using LKDUS_API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LKDUS_API.Controllers
{
    /// <summary>
    /// Endpoint used to interact with the Users in the LKDUS database
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository,
            ILoggerService logger,
            IMapper mapper
            )
        {
            _userRepository = userRepository;
            _logger = logger;
            _mapper = mapper;
        }


        private string GetControllerActionNames()
        {
            var controller = ControllerContext.ActionDescriptor.ControllerName;
            var action = ControllerContext.ActionDescriptor.ActionName;
            return $"{controller}-{action}";
        }

        /// <summary>
        /// Get all Users
        /// </summary>
        /// <returns>List of all Users</returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUsers()
        {
            var location = GetControllerActionNames();
            try 
            {
                _logger.LogInfo($"{location}: Attempted Get All Users");
                var users = await _userRepository.FindAll();
                var response = _mapper.Map<IList<UserDTO>>(users);
                _logger.LogInfo($"{location}: Sucessfully got all Users");
                
                return Ok(response);
            }
            catch(Exception e)
            {
                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }

          
        }

        /// <summary>
        /// Get the User by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>An User's record</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUser(int id)
        {
            var location = GetControllerActionNames();

            try
            {
                _logger.LogInfo($"{location}: Attempted Get User with ID: {id}");
                var user = await _userRepository.FindById(id);
                if(user == null)
                {
                    _logger.LogWarn($"{location}: User with id:{id} was not found");
                    return NotFound();
                }
                var response = _mapper.Map<UserDTO>(user);
                _logger.LogInfo($"{location}: Sucessfully got the user with ID:{id}" );

                return Ok(response);
            }
            catch (Exception e)
            {
                //interpolation for building a string
                return InternalError($"{e.Message} - {e.InnerException}");

              
            }


        }
        /// <summary>
        /// Create a user 
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin, Master")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] UserCreateDTO userDTO)
        {
            

            try
            {
                _logger.LogInfo($"User submission Attempted");
                if (userDTO == null)
                {
                    _logger.LogWarn($"Empty request was submitted");
                    return BadRequest(ModelState);
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogWarn($"User Data was Incomplete");
                    return BadRequest(ModelState);

                }
                var user = _mapper.Map<User>(userDTO);
                var isGood = await _userRepository.Create(user);
                if (!isGood)
                {
                    
                    return InternalError($"User creation failed");
                }

                _logger.LogInfo($"User Data was created");
                return Created("Create", new { user } );
            }
            catch (Exception e)
            {
               return  InternalError($"{e.Message} - {e.InnerException}");
                
            }

        }

         /// <summary>
         /// Updates user with specified Id
         /// </summary>
         /// <param name="id"></param>
         /// <param name="userUpdateDTO"></param>
         /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Master")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] UserUpdateDTO userUpdateDTO)
        {
            try
            {
                _logger.LogWarn($"User update atempted - id: {id}");
                if (userUpdateDTO == null || id <1 || id != userUpdateDTO.Id )
                {
                    _logger.LogWarn($"User update failed with wrong data");
                    return BadRequest(ModelState);
                }

                var isExisting = await _userRepository.isExists(id);

                if (isExisting == false)
                {
                    _logger.LogWarn($"User Data was not found");
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogWarn($"User Data was Incomplete");
                    return BadRequest(ModelState);

                }
                var user = _mapper.Map<User>(userUpdateDTO);
                var isGood = await _userRepository.Update(user);
                if (!isGood)
                {

                    return InternalError($"User update failed");
                }

                _logger.LogInfo($"User Data with id: {id} was updated");
                return NoContent();
                
            }
            catch (Exception e)
            {
                return InternalError($"{e.Message} - {e.InnerException}");

            }

        }


        /// <summary>
        /// Removes user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Master")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _logger.LogWarn($"User deletion atempted - id: {id}");
                if (id < 1 )
                {
                    _logger.LogWarn($"User deleting failed with wrong data");
                    return BadRequest();
                }
                 
                var isExist = await _userRepository.isExists(id);
                 
                if (!isExist)
                {
                    _logger.LogWarn($"User Data with id: {id} was not found");
                    return NotFound();
                }
                var user = await _userRepository.FindById(id);
                var isGood = await _userRepository.Delete(user);

                if (!isGood)
                {
                    return InternalError($"User Delete failed");
                }

                _logger.LogWarn($"User Data with id: {id} was deleted");
                return NoContent();

            }
            catch (Exception e)
            {
                return InternalError($"{e.Message} - {e.InnerException}");

            }

        }

        private ObjectResult InternalError(string message)
        {
            _logger.LogError(message);
            return StatusCode(500, "Something is broken, please contact your supervisor");
        }

    }
}
