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
    /// Interacts with the Measurements Table
    /// </summary>


    [Route("api/[controller]")]
    [ApiController]
    public class MeasurementsController : ControllerBase
    {
        private readonly IMeasurementRepository measurementRepository;

        private readonly ILoggerService logger;
        private readonly IMapper mapper;

        public MeasurementsController(IMeasurementRepository measurementRepository,
            ILoggerService logger,
            IMapper mapper
            )
        {
            this.measurementRepository = measurementRepository;
            this.logger = logger;
            this.mapper = mapper;
        }

        /// <summary>
        /// gets all measurements
        /// </summary>
        /// <returns>A list of all measurements</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMeasurements()
        {
            var location = GetControllerActionNames();
            try
            {
                this.logger.LogInfo($"{location}: Attempted Call");
                var measurements = await this.measurementRepository.FindAll();
                 
                var response = this.mapper.Map<IList<MeasurementDTO>>(measurements);
                this.logger.LogInfo("Sucessfully got all Measurements");

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
        /// Get the User by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>An measurement record by id</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMeasurement(int id)
        {
            var location = GetControllerActionNames();
            try
            {
                this.logger.LogInfo($"{location}: Attempted Get User with ID: {id}");
                var measurement = await this.measurementRepository.FindById(id);

                if(measurement == null)
                {

                    this.logger.LogWarn($"Measurement with the id:{id} was not found");
                    return NotFound();
                }

                var response = this.mapper.Map<MeasurementDTO>(measurement);
                this.logger.LogInfo($"Sucessfully got the measurement with Id:{id}");


                    return Ok(response);
                   
            }
            catch (Exception e)
            {
                //interpolation for building a string
                return InternalError($"{e.Message} - {e.InnerException}");

              
            }


        }

         
        /// <summary>
        /// Creates a new measurement
        /// </summary>
        /// <param name="measurementCreateDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] MeasurementCreateDTO measurementCreateDTO)
        {

            var location = GetControllerActionNames();

            try
            {
                this.logger.LogInfo($"{location}: Measurement creation  Attempted");
                if (measurementCreateDTO == null)
                {
                    this.logger.LogWarn($"{location}: Empty request was submitted");
                    return BadRequest(ModelState);
                }
                if (!ModelState.IsValid)
                {
                    this.logger.LogWarn($"{location}: Measurement Data was Incomplete");
                    return BadRequest(ModelState);

                }
                measurementCreateDTO.DateCreated = DateTime.Now.ToString();
                var measurement = this.mapper.Map<Measurement>(measurementCreateDTO);
                var isGood = await measurementRepository.Create(measurement);
                if (!isGood)
                {
                    
                    return InternalError($"{location}: Measurement creation failed");
                }

                this.logger.LogInfo($"{location}: Measurement creation was created");
                this.logger.LogInfo($"{location}: {measurement}");
                return Created("Create", new { measurement } );
            }
            catch (Exception e)
            {
               return  InternalError($"{location}: {e.Message} - {e.InnerException}");
                
            }

        }
         
         /// <summary>
         /// Updates measurement with specified Id
         /// </summary>
         /// <param name="id"></param>
         /// <param name="measurementUpdateDTO"></param>
         /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(
            int id, 
            [FromBody] MeasurementUpdateDTO measurementUpdateDTO)
        {
            var location = GetControllerActionNames();
            try
            {
                this.logger.LogWarn($"{location}: measurement update atempted - id: {id}");
                if (measurementUpdateDTO == null || id <1  || measurementUpdateDTO.Id < 1 )
                {
                    this.logger.LogWarn($"{location}: measurement update failed with wrong data");
                    return BadRequest(ModelState);
                }

                var isExist = await this.measurementRepository.isExists(id);

                if (!isExist)
                {
                    this.logger.LogWarn($"{location}: measurement Data was not found");
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    this.logger.LogWarn($"{location}: measurement Data was Incomplete");
                    return BadRequest(ModelState);

                }
                measurementUpdateDTO.DateCreated = DateTime.Now.ToString();
                var measurement = this.mapper.Map<Measurement>(measurementUpdateDTO);
                var isGood = await this.measurementRepository.Update(measurement);
                if (!isGood)
                {

                    return InternalError($"{location}: measurement update failed");
                }

                this.logger.LogInfo($"{location}: measurement Data with id: {id} was updated");
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
                this.logger.LogWarn($"{location}: measurement deletion atempted - id: {id}");
                if (id < 1 )
                {
                    this.logger.LogWarn($"{location}: measurement deleting failed with wrong data");
                    return BadRequest();
                }
                 
                var isExist = await this.measurementRepository.isExists(id);
                 
                if (!isExist)
                {
                    this.logger.LogWarn($"{location}: measurement Data with id: {id} was not found");
                    return NotFound();
                }
                var user = await this.measurementRepository.FindById(id);
                var isGood = await this.measurementRepository.Delete(user);

                if (!isGood)
                {
                    return InternalError($"{location}: measurement Delete failed");
                }

                this.logger.LogWarn($"{location}:  measurement Data with id: {id} was deleted");
                return NoContent();

            }
            catch (Exception e)
            {
                return InternalError($"{location}: {e.Message} - {e.InnerException}");

            }

        }
         
          
    }
}
