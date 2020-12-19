using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interacts with the FUS Packs VIEW
    /// </summary>


    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class MachineController : ControllerBase
    {
        private readonly IMachineRepository machineRepository;

        private readonly ILoggerService logger;
        private readonly IMapper mapper;

        public MachineController(IMachineRepository machineRepository,
            ILoggerService logger,
            IMapper mapper
            )
        {
            this.machineRepository = machineRepository;
            this.logger = logger;
            this.mapper = mapper;
        }

        /// <summary>
        /// gets all MACHINES
        /// </summary>
        /// <returns>A list of all machines</returns>
        [HttpGet]
//[Authorize(Roles = "Operator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMachines()
        {
            var location = GetControllerActionNames();
            try
            {
                this.logger.LogInfo($"{location}: Attempted Call");
                var machines = await this.machineRepository.FindAll();
                 
                var response = this.mapper.Map<IList<FusPackDTO>>(machines);
                this.logger.LogInfo("Sucessfully got all machines");

                return Ok(response);
            }
            catch (Exception e)
            {
                return InternalError($"{location}:{e.Message} - {e.InnerException}");
                 
            }
        }


        private ObjectResult InternalError(string message)
        {
            this.logger.LogError(message);
            return StatusCode(500, "Something is broken, please contact your supervisor");
        }


        private string GetControllerActionNames()
        {
            var controller = ControllerContext.ActionDescriptor.ControllerName;
            var action = ControllerContext.ActionDescriptor.ActionName;
            return $"{controller}-{action}";
        }
        
         
         
        /// <summary>
        /// Get the machine   by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>An machine record by id</returns>
        [HttpGet("{id:int}")]
       // [Authorize(Roles = "Operator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMachine(int id)
        {
            var location = GetControllerActionNames();
            try
            {
                this.logger.LogInfo($"{location}: Attempted get machine with ID: {id}");
                var machine = await this.machineRepository.FindById(id);

                if(machine == null)
                {

                    this.logger.LogWarn($"machine with the id:{id} was not found");
                    return NotFound();
                }

                var response = this.mapper.Map<MachineDTO>(machine);
                this.logger.LogInfo($"Sucessfully got the machine with Id:{id}");


                    return Ok(response);
                   
            }
            catch (Exception e)
            {
                //interpolation for building a string
                return InternalError($"{e.Message} - {e.InnerException}");

              
            }


        }


        /// <summary>
        /// Creates a new machine
        /// </summary>
        /// <param name="machineCreateDTO"></param>
        /// <returns></returns>
        [HttpPost]
        // [Authorize(Roles = "Operator")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] MachineCreateDTO machineCreateDTO)
        {

            var location = GetControllerActionNames();

            try
            {
                this.logger.LogInfo($"{location}: Machine creation  Attempted");
                if (machineCreateDTO == null)
                {
                    this.logger.LogWarn($"{location}: Empty request was submitted");
                    return BadRequest(ModelState);
                }
                if (!ModelState.IsValid)
                {
                    this.logger.LogWarn($"{location}: machineCreateDTO Data was Incomplete");
                    return BadRequest(ModelState);

                }
                 
                var machine = this.mapper.Map<Machine>(machineCreateDTO);
                var isGood = await machineRepository.Create(machine );
                if (!isGood)
                {

                    return InternalError($"{location}: Machine creation failed");
                }

                this.logger.LogInfo($"{location}: Machine creation was created");
                this.logger.LogInfo($"{location}: {machine}");
                return Created("Create", new { machine });
            }
            catch (Exception e)
            {
                return InternalError($"{location}: {e.Message} - {e.InnerException}");

            }

        }

        /// <summary>
        /// Updates machine with specified Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="machineUpdateDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        // [Authorize(Roles = "Operator")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] MachineUpdateDTO  machineUpdateDTO)
        {
            var location = GetControllerActionNames();
            try
            {
                this.logger.LogWarn($"{location}: machinE update atempted - id: {id}");
                if (machineUpdateDTO == null || id < 1 || machineUpdateDTO.Id < 1)
                {
                    this.logger.LogWarn($"{location}: machine  update failed with wrong data");
                    return BadRequest(ModelState);
                }

                var isExist = await this.machineRepository.isExists(id);

                if (!isExist)
                {
                    this.logger.LogWarn($"{location}: pack Data was not found");
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    this.logger.LogWarn($"{location}: pack Data was Incomplete");
                    return BadRequest(ModelState);

                }
                 
                var machine = this.mapper.Map<Machine>(machineUpdateDTO);
                var isGood = await this.machineRepository.Update(machine);
                if (!isGood)
                {

                    return InternalError($"{location}: machine update failed");
                }

                this.logger.LogInfo($"{location}: machine Data with id: {id} was updated");
                return NoContent();

            }
            catch (Exception e)
            {
                return InternalError($"{location}: {e.Message} - {e.InnerException}");

            }

        }


        /// <summary>
        /// Removes machine by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {

            var location = GetControllerActionNames();
            try
            {
                this.logger.LogWarn($"{location}: machine deletion atempted - id: {id}");
                if (id < 1)
                {
                    this.logger.LogWarn($"{location}: machine deleting failed with wrong data");
                    return BadRequest();
                }

                var isExist = await this.machineRepository.isExists(id);

                if (!isExist)
                {
                    this.logger.LogWarn($"{location}: machine Data with id: {id} was not found");
                    return NotFound();
                }
                var pack = await this.machineRepository.FindById(id);
                var isGood = await this.machineRepository.Delete(pack);

                if (!isGood)
                {
                    return InternalError($"{location}: machine Delete failed");
                }

                this.logger.LogWarn($"{location}:  machine Data with id: {id} was deleted");
                return NoContent();

            }
            catch (Exception e)
            {
                return InternalError($"{location}: {e.Message} - {e.InnerException}");

            }

        }


    }
}
