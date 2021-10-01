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
    /// Interacts with the Class Table
    /// </summary>


    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ClasssController : ControllerBase
    {
        private readonly IClasssRepository classRepository;
     //   private readonly IMeasurementRepository measurementRepository;

        private readonly ILoggerService logger;
        private readonly IMapper mapper;

        public ClasssController(IClasssRepository classRepository,
           
            ILoggerService logger,
            IMapper mapper
            )
        {
            this.classRepository = classRepository;
           // this.measurementRepository = measurementRepository;
            this.logger = logger;
            this.mapper = mapper;
        }

        /// <summary>
        /// gets all classes
        /// </summary>
        /// <returns>A list of all classes</returns>
        [HttpGet]
//[Authorize(Roles = "Operator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetClasses()
        {
            var location = GetControllerActionNames();
            try
            {
                this.logger.LogInfo($"{location}: Attempted Class");
                var classes = await this.classRepository.FindAll();
                 
                var response = this.mapper.Map<IList<ClassDTO>>(classes);
                this.logger.LogInfo("Sucessfully got all Classes");

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
        /// Get the Class by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>An pack record by id</returns>
        [HttpGet("{id:int}")]
       // [Authorize(Roles = "Operator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetClass(int id)
        {
            var location = GetControllerActionNames();
            try
            {
                this.logger.LogInfo($"{location}: Attempted Get Class with ID: {id}");
                var classs = await this.classRepository.FindById(id);

                if(classs == null)
                {

                    this.logger.LogWarn($"Class with the id:{id} was not found");
                    return NotFound();
                }

                var response = this.mapper.Map<ClassDTO>(classs);
                this.logger.LogInfo($"Sucessfully got the Class with Id:{id}");


                    return Ok(response);
                   
            }
            catch (Exception e)
            {
                //interpolation for building a string
                return InternalError($"{e.Message} - {e.InnerException}");

              
            }


        }


        /// <summary>
        /// Creates a new Class
        /// </summary>
        /// <param name="classCreateDTO"></param>

        /// <returns></returns>
        [HttpPost]
       // [Authorize(Roles = "Operator")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] ClassCreateDTO classCreateDTO)
        {

            var location = GetControllerActionNames();

            try
            {
                this.logger.LogInfo($"{location}: Class creation  Attempted");
                if (classCreateDTO == null)
                {
                    this.logger.LogWarn($"{location}: Empty request was submitted");
                    return BadRequest(ModelState);
                }
                if (!ModelState.IsValid)
                {
                    this.logger.LogWarn($"{location}: Class Data was Incomplete");
                    return BadRequest(ModelState);

                }
               // defectCreateDTO.DateCreated = DateTime.Now;//.ToString();
                var classs = this.mapper.Map<Classs>(classCreateDTO);
               
                
                //foreach( var meas in measurementListCreateDTO)
                //{
                //    var measurement = this.mapper.Map<Measurement>(meas);
                //    var measrementtocreate = await measurementRepository.Create(measurement);
                //}
               
                var isGood = await classRepository.Create(classs);
               
                if (!isGood)
                {
                    
                    return InternalError($"{location}: Class creation failed");
                }

                this.logger.LogInfo($"{location}: Class creation was created");
                this.logger.LogInfo($"{location}: {classs}");
                return Created("Create", new { classs } );
               //return (IActionResult)pack;
            }
            catch (Exception e)
            {
               return  InternalError($"{location}: {e.Message} - {e.InnerException}");
                
            }

        }

        /// <summary>
        /// Updates Class with specified Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="classUpdateDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
       // [Authorize(Roles = "Operator")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(
            int id, 
            [FromBody] ClassUpdateDTO classUpdateDTO)
        {
            var location = GetControllerActionNames();
            try
            {
                this.logger.LogWarn($"{location}: Class update atempted - id: {id}");
                if (classUpdateDTO == null || id <1  || classUpdateDTO.Id < 1 )
                {
                    this.logger.LogWarn($"{location}: Class update failed with wrong data");
                    return BadRequest(ModelState);
                }

                var isExist = await this.classRepository.isExists(id);

                if (!isExist)
                {
                    this.logger.LogWarn($"{location}: Class Data was not found");
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    this.logger.LogWarn($"{location}: Class Data was Incomplete");
                    return BadRequest(ModelState);

                }
              //  defectUpdateDTO.DateCreated = DateTime.Now;//.ToString();
                var classs = this.mapper.Map<Classs>(classUpdateDTO);
                var isGood = await this.classRepository.Update(classs);
                if (!isGood)
                {

                    return InternalError($"{location}: Class update failed");
                }

                this.logger.LogInfo($"{location}: Class Data with id: {id} was updated");
                return NoContent();
                
            }
            catch (Exception e)
            {
                return InternalError($"{location}: {e.Message} - {e.InnerException}");

            }

        }


        /// <summary>
        /// Removes Class by id
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
                this.logger.LogWarn($"{location}: Class deletion atempted - id: {id}");
                if (id < 1 )
                {
                    this.logger.LogWarn($"{location}: Class deleting failed with wrong data");
                    return BadRequest();
                }
                 
                var isExist = await this.classRepository.isExists(id);
                 
                if (!isExist)
                {
                    this.logger.LogWarn($"{location}: pack Data with id: {id} was not found");
                    return NotFound();
                }
                var classs = await this.classRepository.FindById(id);
                var isGood = await this.classRepository.Delete(classs);

                if (!isGood)
                {
                    return InternalError($"{location}: Class Delete failed");
                }

                this.logger.LogWarn($"{location}:  Class Data with id: {id} was deleted");
                return NoContent();

            }
            catch (Exception e)
            {
                return InternalError($"{location}: {e.Message} - {e.InnerException}");

            }

        }
         
          
    }
}
