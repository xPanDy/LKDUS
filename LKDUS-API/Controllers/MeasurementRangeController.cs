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
    public class MeasurementRangeController : ControllerBase
    {
        private readonly IMeasurementRangeRepository  measurementRangeRepository   ;

        private readonly ILoggerService logger;
        private readonly IMapper mapper;

        public MeasurementRangeController(IMeasurementRangeRepository  measurementRangeRepository,
            ILoggerService logger,
            IMapper mapper
            )
        {
            this.measurementRangeRepository = measurementRangeRepository;
            this.logger = logger;
            this.mapper = mapper;
        }

        /// <summary>
        /// gets all measurements rages
        /// </summary>
        /// <returns>A list of all measurements types</returns>
        [HttpGet]
        //[Authorize(Roles = "Operator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMeasurementsRages()
        {
            var location = GetControllerActionNames();
            try
            {
                this.logger.LogInfo($"{location}: Attempted Call");
                var measurementsRange = await this.measurementRangeRepository.FindAll();
                 
                var response = this.mapper.Map<IList<MeasurementRangeDTO>>(measurementsRange);
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
        /// Get the MeasurementRang  by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>An measurement type record by id</returns>
        [HttpGet("{id:int}")]
       // [Authorize(Roles = "Operator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMeasurementRange(int id)
        {
            var location = GetControllerActionNames();
            try
            {
                this.logger.LogInfo($"{location}: Attempted Get measurement range   with ID: {id}");
                var measurementsRange = await this.measurementRangeRepository.FindById(id);

                if(measurementsRange == null)
                {

                    this.logger.LogWarn($"measurement range  with the id:{id} was not found");
                    return NotFound();
                }

                var response = this.mapper.Map<MeasurementRangeDTO>(measurementsRange);
                this.logger.LogInfo($"Sucessfully got the measurement range   with Id:{id}");


                    return Ok(response);
                   
            }
            catch (Exception e)
            {
                //interpolation for building a string
                return InternalError($"{e.Message} - {e.InnerException}");

              
            }


        }


        /// <summary>
        /// Creates a new measurementsRange
        /// </summary>
        /// <param name="measurementRangeCreateDTO"></param>
        /// <returns></returns>
        [HttpPost]
       // [Authorize(Roles = "Operator")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] MeasurementRangeCreateDTO  measurementRangeCreateDTO)
        {

            var location = GetControllerActionNames();

            try
            {
                this.logger.LogInfo($"{location}: Measurement range creation  Attempted");
                if (measurementRangeCreateDTO == null)
                {
                    this.logger.LogWarn($"{location}: Empty request was submitted");
                    return BadRequest(ModelState);
                }
                if (!ModelState.IsValid)
                {
                    this.logger.LogWarn($"{location}: Measurement  range Data was Incomplete");
                    return BadRequest(ModelState);

                }
               // measurementTypeCreateDTO.DateCreated = DateTime.Now.ToString();
                var measurementRange  = this.mapper.Map<MeasurementRange>(measurementRangeCreateDTO);
                var isGood = await measurementRangeRepository.Create(measurementRange);
                if (!isGood)
                {
                    
                    return InternalError($"{location}: measurementRange type creation failed");
                }

                this.logger.LogInfo($"{location}: measurementRangecreation was created");
                this.logger.LogInfo($"{location}: {measurementRange}");
                return Created("Create", new { measurementRange } );
            }
            catch (Exception e)
            {
               return  InternalError($"{location}: {e.Message} - {e.InnerException}");
                
            }

        }

        /// <summary>
        /// Updates measurementRange with specified Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="measurementRangeUpdateDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
       // [Authorize(Roles = "Operator")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(
            int id, 
            [FromBody] MeasurementRangeUpdateDTO  measurementRangeUpdateDTO)
        {
            var location = GetControllerActionNames();
            try
            {
                this.logger.LogWarn($"{location}:measurementRangeUpdateDTO update atempted - id: {id}");
                if (measurementRangeUpdateDTO == null || id <1  || measurementRangeUpdateDTO.Id < 1 )
                { 
                    this.logger.LogWarn($"{location}: measurementRangeUpdateDTOE update failed with wrong data");
                    return BadRequest(ModelState);
                }

                var isExist = await this.measurementRangeRepository.isExists(id);

                if (!isExist)
                {
                    this.logger.LogWarn($"{location}: measurementRange  Data was not found");
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    this.logger.LogWarn($"{location}: measurement range Data was Incomplete");
                    return BadRequest(ModelState);

                }
               // measurementTypeUpdateDTO.DateCreated = DateTime.Now.ToString();
                var measurementRange = this.mapper.Map<MeasurementRange>(measurementRangeUpdateDTO);
                var isGood = await this.measurementRangeRepository.Update(measurementRange);
                if (!isGood)
                {

                    return InternalError($"{location}: measurement range update failed");
                }

                this.logger.LogInfo($"{location}: measurement range Data with id: {id} was updated");
                return NoContent();
                
            }
            catch (Exception e)
            {
                return InternalError($"{location}: {e.Message} - {e.InnerException}");

            }

        }

        
        /// <summary>
        /// Removes measurement range by id
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
                this.logger.LogWarn($"{location}: measurement range deletion atempted - id: {id}");
                if (id < 1 )
                {
                    this.logger.LogWarn($"{location}: measurement range  deleting failed with wrong data");
                    return BadRequest();
                }
                 
                var isExist = await this.measurementRangeRepository.isExists(id);
                 
                if (!isExist)
                {
                    this.logger.LogWarn($"{location}: measurement range Data with id: {id} was not found");
                    return NotFound();
                }
                var user = await this.measurementRangeRepository.FindById(id);
                var isGood = await this.measurementRangeRepository.Delete(user);

                if (!isGood)
                {
                    return InternalError($"{location}: measurement range Delete failed");
                }

                this.logger.LogWarn($"{location}:  measurement range Data with id: {id} was deleted");
                return NoContent();

            }
            catch (Exception e)
            {
                return InternalError($"{location}: {e.Message} - {e.InnerException}");

            }

        }
         
          
    }
}
