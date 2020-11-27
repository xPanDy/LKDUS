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
    /// Interacts with the Measurements Table
    /// </summary>


    [Route("api/[controller]")]
    [ApiController]
  //  [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public class MeasurementPositionController : ControllerBase
    {
        private readonly IMeasurementPositionRepository measurementPositionRepository;

        private readonly ILoggerService logger;
        private readonly IMapper mapper;

        public MeasurementPositionController(IMeasurementPositionRepository measurementPositionRepository,
            ILoggerService logger,
            IMapper mapper
            )
        {
            this.measurementPositionRepository = measurementPositionRepository;
            this.logger = logger;
            this.mapper = mapper;
        }

        /// <summary>
        /// gets all measurement positions
        /// </summary>
        /// <returns>A list of all measurements positions</returns>
        [HttpGet]
        //[Authorize(Roles = "Operator")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMeasurementPositions()
        {
            var location = GetControllerActionNames();
            try
            {
                this.logger.LogInfo($"{location}: Attempted Call");
                var measurementPosition = await this.measurementPositionRepository.FindAll();
                 
                var response = this.mapper.Map<IList<MeasurementPositionDTO>>(measurementPosition);
                this.logger.LogInfo("Sucessfully got all measurementPositions");

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
        /// Get the Measurement Position by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>An measurement position record by id</returns>
        [HttpGet("{id:int}")]
        [Authorize(Roles = "Operator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMeasurementPosition(int id)
        {
            var location = GetControllerActionNames();
            try
            {
                this.logger.LogInfo($"{location}: Attempted Get Measurement position with ID: {id}");
                var measurementPosition = await this.measurementPositionRepository.FindById(id);

                if(measurementPosition == null)
                {

                    this.logger.LogWarn($"Measurement position with the id:{id} was not found");
                    return NotFound();
                }

                var response = this.mapper.Map<MeasurementPositionDTO>(measurementPosition);
                this.logger.LogInfo($"Sucessfully got the measurement position with Id:{id}");


                    return Ok(response);
                   
            }
            catch (Exception e)
            {
                //interpolation for building a string
                return InternalError($"{e.Message} - {e.InnerException}");

              
            }


        }


        /// <summary>
        /// Creates a new measurement position
        /// </summary>
        /// <param name="measurementPositionCreateDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Operator")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] MeasurementPositionCreateDTO measurementPositionCreateDTO)
        {

            var location = GetControllerActionNames();

            try
            {
                this.logger.LogInfo($"{location}: Measurement  position creation  Attempted");
                if (measurementPositionCreateDTO == null)
                {
                    this.logger.LogWarn($"{location}: Empty request was submitted");
                    return BadRequest(ModelState);
                }
                if (!ModelState.IsValid)
                {
                    this.logger.LogWarn($"{location}: Measurement Data was Incomplete");
                    return BadRequest(ModelState);

                }
               // measurementPositionCreateDTO.DateCreated = DateTime.Now.ToString();
                var measurementPosition = this.mapper.Map<MeasurementPosition>(measurementPositionCreateDTO);
                var isGood = await measurementPositionRepository.Create(measurementPosition);
                if (!isGood)
                {
                    
                    return InternalError($"{location}: Measurement position creation failed");
                }

                this.logger.LogInfo($"{location}: Measurement position creation was created");
                this.logger.LogInfo($"{location}: {measurementPosition}");
                return Created("Create", new { measurementPosition } );
            }
            catch (Exception e)
            {
               return  InternalError($"{location}: {e.Message} - {e.InnerException}");
                
            }

        }
         
         /// <summary>
         /// Updates measurement position with specified Id
         /// </summary>
         /// <param name="id"></param>
         /// <param name="measurementPositionUpdateDTO"></param>
         /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Operator")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(
            int id, 
            [FromBody] MeasurementPositionUpdateDTO measurementPositionUpdateDTO)
        {
            var location = GetControllerActionNames();
            try
            {
                this.logger.LogWarn($"{location}: measurement position update atempted - id: {id}");
                if (measurementPositionUpdateDTO == null || id <1  || measurementPositionUpdateDTO.Id < 1 )
                {
                    this.logger.LogWarn($"{location}: measurement position update failed with wrong data");
                    return BadRequest(ModelState);
                }

                var isExist = await this.measurementPositionRepository.isExists(id);

                if (!isExist)
                {
                    this.logger.LogWarn($"{location}: measurement position Data was not found");
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    this.logger.LogWarn($"{location}: measurement position Data was Incomplete");
                    return BadRequest(ModelState);

                }
                //measurementPositionUpdateDTO.DateCreated = DateTime.Now.ToString();
                var measurementPosition = this.mapper.Map<MeasurementPosition>(measurementPositionUpdateDTO);
                var isGood = await this.measurementPositionRepository.Update(measurementPosition);
                if (!isGood)
                {

                    return InternalError($"{location}: measurement position update failed");
                }

                this.logger.LogInfo($"{location}: measurement pos Data with id: {id} was updated");
                return NoContent();
                
            }
            catch (Exception e)
            {
                return InternalError($"{location}: {e.Message} - {e.InnerException}");

            }

        }

        
        /// <summary>
        /// Removes measurement by id
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
                this.logger.LogWarn($"{location}: measurement position deletion atempted - id: {id}");
                if (id < 1 )
                {
                    this.logger.LogWarn($"{location}: measurement position deleting failed with wrong data");
                    return BadRequest();
                }
                 
                var isExist = await this.measurementPositionRepository.isExists(id);
                 
                if (!isExist)
                {
                    this.logger.LogWarn($"{location}: measurement position Data with id: {id} was not found");
                    return NotFound();
                }
                var user = await this.measurementPositionRepository.FindById(id);
                var isGood = await this.measurementPositionRepository.Delete(user);

                if (!isGood)
                {
                    return InternalError($"{location}: measurement position Delete failed");
                }

                this.logger.LogWarn($"{location}:  measurement position Data with id: {id} was deleted");
                return NoContent();

            }
            catch (Exception e)
            {
                return InternalError($"{location}: {e.Message} - {e.InnerException}");

            }

        }
         
          
    }
}
