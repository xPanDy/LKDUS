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
    /// Interacts with the Packs Table
    /// </summary>


    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class DefectController : ControllerBase
    {
        private readonly IDefectRepository defectRepository;
     //   private readonly IMeasurementRepository measurementRepository;

        private readonly ILoggerService logger;
        private readonly IMapper mapper;

        public DefectController(IDefectRepository defectRepository,
           
            ILoggerService logger,
            IMapper mapper
            )
        {
            this.defectRepository = defectRepository;
           // this.measurementRepository = measurementRepository;
            this.logger = logger;
            this.mapper = mapper;
        }

        /// <summary>
        /// gets all defects
        /// </summary>
        /// <returns>A list of all defects</returns>
        [HttpGet]
//[Authorize(Roles = "Operator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDefects()
        {
            var location = GetControllerActionNames();
            try
            {
                this.logger.LogInfo($"{location}: Attempted Call");
                var defects = await this.defectRepository.FindAll();
                 
                var response = this.mapper.Map<IList<DefectDTO>>(defects);
                this.logger.LogInfo("Sucessfully got all defects");

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
        /// Get the defect by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>An pack record by id</returns>
        [HttpGet("{id:int}")]
       // [Authorize(Roles = "Operator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDefect(int id)
        {
            var location = GetControllerActionNames();
            try
            {
                this.logger.LogInfo($"{location}: Attempted Get pack with ID: {id}");
                var defect = await this.defectRepository.FindById(id);

                if(defect == null)
                {

                    this.logger.LogWarn($"defect with the id:{id} was not found");
                    return NotFound();
                }

                var response = this.mapper.Map<DefectDTO>(defect);
                this.logger.LogInfo($"Sucessfully got the pack with Id:{id}");


                    return Ok(response);
                   
            }
            catch (Exception e)
            {
                //interpolation for building a string
                return InternalError($"{e.Message} - {e.InnerException}");

              
            }


        }


        /// <summary>
        /// Creates a new defect
        /// </summary>
        /// <param name="defectCreateDTO"></param>

        /// <returns></returns>
        [HttpPost]
       // [Authorize(Roles = "Operator")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] DefectCreateDTO defectCreateDTO)
        {

            var location = GetControllerActionNames();

            try
            {
                this.logger.LogInfo($"{location}: Pack creation  Attempted");
                if (defectCreateDTO == null)
                {
                    this.logger.LogWarn($"{location}: Empty request was submitted");
                    return BadRequest(ModelState);
                }
                if (!ModelState.IsValid)
                {
                    this.logger.LogWarn($"{location}: Pack Data was Incomplete");
                    return BadRequest(ModelState);

                }
               // defectCreateDTO.DateCreated = DateTime.Now;//.ToString();
                var defect = this.mapper.Map<Defect>(defectCreateDTO);
               
                
                //foreach( var meas in measurementListCreateDTO)
                //{
                //    var measurement = this.mapper.Map<Measurement>(meas);
                //    var measrementtocreate = await measurementRepository.Create(measurement);
                //}
               
                var isGood = await defectRepository.Create(defect);
               
                if (!isGood)
                {
                    
                    return InternalError($"{location}: defect creation failed");
                }

                this.logger.LogInfo($"{location}: Pack creation was created");
                this.logger.LogInfo($"{location}: {defect}");
                return Created("Create", new { defect } );
               //return (IActionResult)pack;
            }
            catch (Exception e)
            {
               return  InternalError($"{location}: {e.Message} - {e.InnerException}");
                
            }

        }

        /// <summary>
        /// Updates defect with specified Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="defectUpdateDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
       // [Authorize(Roles = "Operator")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(
            int id, 
            [FromBody] DefectUpdateDTO defectUpdateDTO)
        {
            var location = GetControllerActionNames();
            try
            {
                this.logger.LogWarn($"{location}: defect update atempted - id: {id}");
                if (defectUpdateDTO == null || id <1  || defectUpdateDTO.Id < 1 )
                {
                    this.logger.LogWarn($"{location}: defect update failed with wrong data");
                    return BadRequest(ModelState);
                }

                var isExist = await this.defectRepository.isExists(id);

                if (!isExist)
                {
                    this.logger.LogWarn($"{location}: defect Data was not found");
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    this.logger.LogWarn($"{location}: defect Data was Incomplete");
                    return BadRequest(ModelState);

                }
              //  defectUpdateDTO.DateCreated = DateTime.Now;//.ToString();
                var defect = this.mapper.Map<Defect>(defectUpdateDTO);
                var isGood = await this.defectRepository.Update(defect);
                if (!isGood)
                {

                    return InternalError($"{location}: defect update failed");
                }

                this.logger.LogInfo($"{location}: defect Data with id: {id} was updated");
                return NoContent();
                
            }
            catch (Exception e)
            {
                return InternalError($"{location}: {e.Message} - {e.InnerException}");

            }

        }

        
        /// <summary>
        /// Removes defect by id
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
                this.logger.LogWarn($"{location}: defect deletion atempted - id: {id}");
                if (id < 1 )
                {
                    this.logger.LogWarn($"{location}: defect deleting failed with wrong data");
                    return BadRequest();
                }
                 
                var isExist = await this.defectRepository.isExists(id);
                 
                if (!isExist)
                {
                    this.logger.LogWarn($"{location}: pack Data with id: {id} was not found");
                    return NotFound();
                }
                var pack = await this.defectRepository.FindById(id);
                var isGood = await this.defectRepository.Delete(pack);

                if (!isGood)
                {
                    return InternalError($"{location}: defect Delete failed");
                }

                this.logger.LogWarn($"{location}:  defect Data with id: {id} was deleted");
                return NoContent();

            }
            catch (Exception e)
            {
                return InternalError($"{location}: {e.Message} - {e.InnerException}");

            }

        }
         
          
    }
}
