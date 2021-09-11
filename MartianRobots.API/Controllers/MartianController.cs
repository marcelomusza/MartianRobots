using MartianRobots.Application.Interfaces;
using MartianRobots.Application.Models;
using Microsoft.AspNetCore.Mvc;

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

        // GET: api/<MarsController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<MarsController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        [HttpPost]
        [Route("Input")]
        public IActionResult Input([FromBody] UserInput input)
        {
            //invoke Engine method
            var result = martianEngine.OperateRobotsOnGrid(input);

            if (result != null)
                return Ok(result);
            else
                return BadRequest("Problem found processing robot operations");

        }

        //// PUT api/<MarsController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<MarsController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
