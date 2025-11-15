using Microsoft.AspNetCore.Mvc;
using Repositories;
using Entities;
using Services;
using System.Collections.Generic;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersPasswordController : ControllerBase
    {
        UserPasswordService userPasswordService = new UserPasswordService();

        // GET: api/<UsersPasswordController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UsersPasswordController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UsersPasswordController>
        [HttpPost]
        public ActionResult<int>checkPassword([FromBody] UserPassword password)
        {
            int score = userPasswordService.checkPassword(password.Password);
            if (score > 2)
                return Ok(score);
            return BadRequest();
        }

        // PUT api/<UsersPasswordController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsersPasswordController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
