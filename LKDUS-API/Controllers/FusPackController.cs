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
    public class FusPackController : ControllerBase
    {
        private readonly IFusPackRepository fusPackRepository;

        private readonly ILoggerService logger;
        private readonly IMapper mapper;

        public FusPackController(IFusPackRepository fusPackRepository,
            ILoggerService logger,
            IMapper mapper
            )
        {
            this.fusPackRepository = fusPackRepository;
            this.logger = logger;
            this.mapper = mapper;
        }

        /// <summary>
        /// gets all packs
        /// </summary>
        /// <returns>A list of all fus  packs</returns>
        [HttpGet]
//[Authorize(Roles = "Operator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetFusPacks()
        {
            var location = GetControllerActionNames();
            try
            {
                this.logger.LogInfo($"{location}: Attempted Call");
                var packs = await this.fusPackRepository.FindAll();
                 
                var response = this.mapper.Map<IList<FusPackDTO>>(packs);
                this.logger.LogInfo("Sucessfully got all FUS packs");

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
        /// Get the FUS pack by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>An FUS pack record by id</returns>
        [HttpGet("{id:int}")]
       // [Authorize(Roles = "Operator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPack(int id)
        {
            var location = GetControllerActionNames();
            try
            {
                this.logger.LogInfo($"{location}: Attempted Get FUS pack with ID: {id}");
                var pack = await this.fusPackRepository.FindById(id);

                if(pack == null)
                {

                    this.logger.LogWarn($"Pack with the id:{id} was not found");
                    return NotFound();
                }

                var response = this.mapper.Map<FusPackDTO>(pack);
                this.logger.LogInfo($"Sucessfully got the pack with Id:{id}");


                    return Ok(response);
                   
            }
            catch (Exception e)
            {
                //interpolation for building a string
                return InternalError($"{e.Message} - {e.InnerException}");

              
            }


        }

         
       // /// <summary>
       // /// Creates a new pack
       // /// </summary>
       // /// <param name="packCreateDTO"></param>
       // /// <returns></returns>
       // [HttpPost]
       //// [Authorize(Roles = "Operator")]
       // [ProducesResponseType(StatusCodes.Status201Created)]
       // [ProducesResponseType(StatusCodes.Status400BadRequest)]
       // [ProducesResponseType(StatusCodes.Status500InternalServerError)]
       // public async Task<IActionResult> Create([FromBody] PackCreateDTO packCreateDTO)
       // {

       //     var location = GetControllerActionNames();

       //     try
       //     {
       //         this.logger.LogInfo($"{location}: Pack creation  Attempted");
       //         if (packCreateDTO == null)
       //         {
       //             this.logger.LogWarn($"{location}: Empty request was submitted");
       //             return BadRequest(ModelState);
       //         }
       //         if (!ModelState.IsValid)
       //         {
       //             this.logger.LogWarn($"{location}: Pack Data was Incomplete");
       //             return BadRequest(ModelState);

       //         }
       //         packCreateDTO.DateCreated = DateTime.Now.ToString();
       //         var pack = this.mapper.Map<FusPack>(packCreateDTO);
       //         var isGood = await fusPackRepository.Create(pack);
       //         if (!isGood)
       //         {
                    
       //             return InternalError($"{location}: Pack creation failed");
       //         }

       //         this.logger.LogInfo($"{location}: Pack creation was created");
       //         this.logger.LogInfo($"{location}: {pack}");
       //         return Created("Create", new { pack } );
       //     }
       //     catch (Exception e)
       //     {
       //        return  InternalError($"{location}: {e.Message} - {e.InnerException}");
                
       //     }

       // }

       // /// <summary>
       // /// Updates pack with specified Id
       // /// </summary>
       // /// <param name="id"></param>
       // /// <param name="packUpdateDTO"></param>
       // /// <returns></returns>
       // [HttpPut("{id}")]
       //// [Authorize(Roles = "Operator")]
       // [ProducesResponseType(StatusCodes.Status204NoContent)]
       // [ProducesResponseType(StatusCodes.Status400BadRequest)]
       // [ProducesResponseType(StatusCodes.Status500InternalServerError)]
       // public async Task<IActionResult> Update(
       //     int id, 
       //     [FromBody] PackUpdateDTO packUpdateDTO)
       // {
       //     var location = GetControllerActionNames();
       //     try
       //     {
       //         this.logger.LogWarn($"{location}: pack update atempted - id: {id}");
       //         if (packUpdateDTO == null || id <1  || packUpdateDTO.Id < 1 )
       //         {
       //             this.logger.LogWarn($"{location}: pack update failed with wrong data");
       //             return BadRequest(ModelState);
       //         }

       //         var isExist = await this.packRepository.isExists(id);

       //         if (!isExist)
       //         {
       //             this.logger.LogWarn($"{location}: pack Data was not found");
       //             return NotFound();
       //         }
       //         if (!ModelState.IsValid)
       //         {
       //             this.logger.LogWarn($"{location}: pack Data was Incomplete");
       //             return BadRequest(ModelState);

       //         }
       //         packUpdateDTO.DateCreated = DateTime.Now.ToString();
       //         var pack = this.mapper.Map<Pack>(packUpdateDTO);
       //         var isGood = await this.packRepository.Update(pack);
       //         if (!isGood)
       //         {

       //             return InternalError($"{location}: pack update failed");
       //         }

       //         this.logger.LogInfo($"{location}: pack Data with id: {id} was updated");
       //         return NoContent();
                
       //     }
       //     catch (Exception e)
       //     {
       //         return InternalError($"{location}: {e.Message} - {e.InnerException}");

       //     }

       // }

        
       // /// <summary>
       // /// Removes pack by id
       // /// </summary>
       // /// <param name="id"></param>
       // /// <returns></returns>
       // [HttpDelete("{id}")]
       // [ProducesResponseType(StatusCodes.Status204NoContent)]
       // [ProducesResponseType(StatusCodes.Status400BadRequest)]
       // [ProducesResponseType(StatusCodes.Status500InternalServerError)]
       // public async Task<IActionResult> Delete(int id)
       // {

       //     var location = GetControllerActionNames();
       //     try
       //     {
       //         this.logger.LogWarn($"{location}: pack deletion atempted - id: {id}");
       //         if (id < 1 )
       //         {
       //             this.logger.LogWarn($"{location}: pack deleting failed with wrong data");
       //             return BadRequest();
       //         }
                 
       //         var isExist = await this.packRepository.isExists(id);
                 
       //         if (!isExist)
       //         {
       //             this.logger.LogWarn($"{location}: pack Data with id: {id} was not found");
       //             return NotFound();
       //         }
       //         var pack = await this.packRepository.FindById(id);
       //         var isGood = await this.packRepository.Delete(pack);

       //         if (!isGood)
       //         {
       //             return InternalError($"{location}: pack Delete failed");
       //         }

       //         this.logger.LogWarn($"{location}:  pack Data with id: {id} was deleted");
       //         return NoContent();

       //     }
       //     catch (Exception e)
       //     {
       //         return InternalError($"{location}: {e.Message} - {e.InnerException}");

       //     }

       // }
         
          
    }
}
