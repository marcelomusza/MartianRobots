using MartianRobots.Application.DTOs;
using MartianRobots.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MartianRobots.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MartianController : ControllerBase
    {

        private IMartianEngine martianEngine;

        public MartianController(IMartianEngine MartianEngine)
        {
            martianEngine = MartianEngine;
        }

        //GET: api/<MarsController>
        [HttpGet]
        [Route("Test")]
        public IActionResult Test()
        {
            return Ok("All Good");
        }

        [HttpPost]
        [Route("Input")]
        public IActionResult Input([FromBody] UserInputDTO input)
        {            
            //It is assumed that the input data is already validated at the client side, in order to avoid postbacks
            //and guaranteeing consistent information to process in the backend              

            //invoke Engine method
            var result = martianEngine.OperateRobotsOnGrid(input);

            if (result != null)
                return Ok(result);
            else
                return BadRequest("Problem found processing robot operations");

        }

        [HttpGet]
        [Route("GetRobotMovements")]
        public IActionResult GetRobotMovements()
        {
            //Example Method to retrieve all robot movements, their orientation, instructions, and positions


            //invoke Engine method
            var result = martianEngine.GetRobotMovements();

            if (result != null)
                return Ok(result);
            else
                return BadRequest("Problem found processing robot operations");

        }


    }
}
