using LoginApiService.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LoginApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        // GET: api/<LoginController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }


        // POST api/<LoginController>
        [HttpPost("Register")]
        public IActionResult Register([FromBody] Register user)
        {
            Register reg = new Register();

            int res = reg.RegisterUser(user);

            if (res == 1)
            {
                return Ok(new { StatusCode = 200 });
            }
            else
            {
                return Ok(new { StatusCode = 400 });
            }
        }


        [HttpPost("Login")]
        public IActionResult Login([FromBody] Register user)
        {
            Register reg = new Register();

            int res = reg.LoginUser(user);

            if (res > 0)
            {
                return Ok(new { StatusCode = 200 });
            }
            else
            {
                return Ok(new { StatusCode = 400 });
            }
        }


        [HttpPost("GetUserID")]
        public IActionResult GetUserID([FromBody] Register user)
        {
            Register reg = new Register();

            int res = reg.GetUserID(user);

            if (res > 0)
            {
                return Ok(new { StatusCode = 200 , UserID = res});
            }
            else
            {
                return Ok(new { StatusCode = 400 });
            }
        }


        // PUT api/<LoginController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LoginController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
