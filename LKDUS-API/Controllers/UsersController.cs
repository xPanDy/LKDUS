using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LKDUS_API.Contracts;
using LKDUS_API.Data;
using LKDUS_API.DTOs;
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
        /// <summary>
        /// Get all Users
        /// </summary>
        /// <returns>List of all Users</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUsers()
        {
            try 
            {
                _logger.LogInfo("Attempted Get All Users");
                var users = await _userRepository.FindAll();
                var response = _mapper.Map<IList<UserDTO>>(users);
                _logger.LogInfo("Sucessfully got all Users");
                
                return Ok(response);
            }
            catch(Exception e)
            {
                return InternalError($"{e.Message} - {e.InnerException}");
            }

          
        }

        /// <summary>
        /// Get the User by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>An User's record</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUser(int id)
        {
            try
            {
                _logger.LogInfo($"Attempted Get User with ID: {id}");
                var user = await _userRepository.FindById(id);
                if(user == null)
                {
                    _logger.LogWarn($"User with id:{id} was not found");
                    return NotFound();
                }
                var response = _mapper.Map<UserDTO>(user);
                _logger.LogInfo($"Sucessfully got the user with ID:{id}" );

                return Ok(response);
            }
            catch (Exception e)
            {
                //interpolation for building a string
                return InternalError($"{e.Message} - {e.InnerException}");

              
            }


        }

        [HttpPost]
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


        private ObjectResult InternalError(string message)
        {
            _logger.LogError(message);
            return StatusCode(500, "Something is broken, please contact your supervisor");
        }

    }
}
