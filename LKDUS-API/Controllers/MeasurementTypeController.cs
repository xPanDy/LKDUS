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
    /// Interacts with the Measurements type Table
    /// </summary>


    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class MeasurementsTypeController : ControllerBase
    {
        private readonly IMeasurementTypeRepository measurementTypeRepository ;

        private readonly ILoggerService logger;
        private readonly IMapper mapper;

        public MeasurementsTypeController(IMeasurementTypeRepository measurementTypeRepository,
            ILoggerService logger,
            IMapper mapper
            )
        {
            this.measurementTypeRepository = measurementTypeRepository;
            this.logger = logger;
            this.mapper = mapper;
        }

        /// <summary>
        /// gets all measurements types
        /// </summary>
        /// <returns>A list of all measurements types</returns>
        [HttpGet]
//[Authorize(Roles = "Operator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMeasurementsType()
        {
            var location = GetControllerActionNames();
            try
            {
                this.logger.LogInfo($"{location}: Attempted Call");
                var measurementsType = await this.measurementTypeRepository.FindAll();
                 
                var response = this.mapper.Map<IList<MeasurementTypeDTO>>(measurementsType);
                this.logger.LogInfo("Sucessfully got all Measurements types");

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
        /// Get the measurement type by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>An measurement type record by id</returns>
        [HttpGet("{id:int}")]
       // [Authorize(Roles = "Operator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMeasurementType(int id)
        {
            var location = GetControllerActionNames();
            try
            {
                this.logger.LogInfo($"{location}: Attempted Get measurement type with ID: {id}");
                var measurementType= await this.measurementTypeRepository.FindById(id);

                if(measurementType == null)
                {

                    this.logger.LogWarn($"Measurement type with the id:{id} was not found");
                    return NotFound();
                }

                var response = this.mapper.Map<MeasurementTypeDTO>(measurementType);
                this.logger.LogInfo($"Sucessfully got the measurement type with Id:{id}");


                    return Ok(response);
                   
            }
            catch (Exception e)
            {
                //interpolation for building a string
                return InternalError($"{e.Message} - {e.InnerException}");

              
            }


        }

         
        /// <summary>
        /// Creates a new measurement type
        /// </summary>
        /// <param name="measurementTypeCreateDTO"></param>
        /// <returns></returns>
        [HttpPost]
       // [Authorize(Roles = "Operator")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] MeasurementTypeCreateDTO measurementTypeCreateDTO)
        {

            var location = GetControllerActionNames();

            try
            {
                this.logger.LogInfo($"{location}: Measurement TYPE creation  Attempted");
                if (measurementTypeCreateDTO == null)
                {
                    this.logger.LogWarn($"{location}: Empty request was submitted");
                    return BadRequest(ModelState);
                }
                if (!ModelState.IsValid)
                {
                    this.logger.LogWarn($"{location}: Measurement  type Data was Incomplete");
                    return BadRequest(ModelState);

                }
               // measurementTypeCreateDTO.DateCreated = DateTime.Now.ToString();
                var measurementType = this.mapper.Map<MeasurementType>(measurementTypeCreateDTO);
                var isGood = await measurementTypeRepository.Create(measurementType);
                if (!isGood)
                {
                    
                    return InternalError($"{location}: Measurement type creation failed");
                }

                this.logger.LogInfo($"{location}: Measurement type creation was created");
                this.logger.LogInfo($"{location}: {measurementType}");
                return Created("Create", new { measurementType } );
            }
            catch (Exception e)
            {
               return  InternalError($"{location}: {e.Message} - {e.InnerException}");
                
            }

        }
         
         /// <summary>
         /// Updates measurement type with specified Id
         /// </summary>
         /// <param name="id"></param>
         /// <param name="measurementTypeUpdateDTO"></param>
         /// <returns></returns>
        [HttpPut("{id}")]
       // [Authorize(Roles = "Operator")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(
            int id, 
            [FromBody] MeasurementTypeUpdateDTO measurementTypeUpdateDTO)
        {
            var location = GetControllerActionNames();
            try
            {
                this.logger.LogWarn($"{location}: measurement TYPE update atempted - id: {id}");
                if (measurementTypeUpdateDTO == null || id <1  || measurementTypeUpdateDTO.Id < 1 )
                {
                    this.logger.LogWarn($"{location}: measurement TYPE update failed with wrong data");
                    return BadRequest(ModelState);
                }

                var isExist = await this.measurementTypeRepository.isExists(id);

                if (!isExist)
                {
                    this.logger.LogWarn($"{location}: measurement TYPE Data was not found");
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    this.logger.LogWarn($"{location}: measurement type Data was Incomplete");
                    return BadRequest(ModelState);

                }
               // measurementTypeUpdateDTO.DateCreated = DateTime.Now.ToString();
                var measurementType = this.mapper.Map<MeasurementType>(measurementTypeUpdateDTO);
                var isGood = await this.measurementTypeRepository.Update(measurementType);
                if (!isGood)
                {

                    return InternalError($"{location}: measurement type update failed");
                }

                this.logger.LogInfo($"{location}: measurement type Data with id: {id} was updated");
                return NoContent();
                
            }
            catch (Exception e)
            {
                return InternalError($"{location}: {e.Message} - {e.InnerException}");

            }

        }

        
        /// <summary>
        /// Removes measurement type by id
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
                this.logger.LogWarn($"{location}: measurement type deletion atempted - id: {id}");
                if (id < 1 )
                {
                    this.logger.LogWarn($"{location}: measurement type  deleting failed with wrong data");
                    return BadRequest();
                }
                 
                var isExist = await this.measurementTypeRepository.isExists(id);
                 
                if (!isExist)
                {
                    this.logger.LogWarn($"{location}: measurement type Data with id: {id} was not found");
                    return NotFound();
                }
                var user = await this.measurementTypeRepository.FindById(id);
                var isGood = await this.measurementTypeRepository.Delete(user);

                if (!isGood)
                {
                    return InternalError($"{location}: measurement type Delete failed");
                }

                this.logger.LogWarn($"{location}:  measurement type Data with id: {id} was deleted");
                return NoContent();

            }
            catch (Exception e)
            {
                return InternalError($"{location}: {e.Message} - {e.InnerException}");

            }

        }
         
          
    }
}
