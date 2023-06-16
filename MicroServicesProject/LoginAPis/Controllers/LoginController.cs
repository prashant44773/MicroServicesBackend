using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using KeyCloakApi.Models;
using LoginData.Models;
using System.IdentityModel.Tokens.Jwt;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KeyCloakApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        // GET api/<LoginController>
        [HttpGet]
        public string Get()
        {
            return "Hello From SErvices Api";
        }

        // GET: api/<LoginController>
        [HttpGet("Token")]
        public async Task<string> Token()
        {
            Service ser = new Service();
            
            string result = await ser.AdminToken();

            /*string Token = JObject.Parse(result)["access_token"].ToString();*/

            return result;
        }

        // GET api/<LoginController>
        [HttpGet("UserList")]
        public async Task<string> UserList()
        {
            Service ser = new Service();

            string Bearer = await ser.AdminToken();

            string result = await ser.GetUserList(Bearer);

            return result;
        }


        // POST api/<LoginController>
        [HttpPost("AddUser")]
        /*public async Task<string> AddUser()*/
        public async Task<string> AddUser([FromBody]Register data)
        {
            Service ser = new Service();

            string Bearer = await ser.AdminToken();

            string result = await ser.AddNewUser(Bearer,data);

            return result;
        }


        // PUT api/<LoginController>/5
        [HttpPost("LoginUser")]
        public async Task<IActionResult> LoginUser([FromBody]Login data)
        {
            Service ser = new Service();

/*            string Bearer = await ser.AdminToken();
*/
            string result = await ser.LoginUserToken(data);

/*            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = (JwtSecurityToken)tokenHandler.ReadToken(result);
            string role = securityToken.Claims.First(claim => claim.Type == "roles").Value;*/

            string role = ser.getJWTTokenClaim(result,"roles");

            return Ok( new { status=200 , Token = result , Roles = role} );
        }

        // DELETE api/<LoginController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
   
    }
}
