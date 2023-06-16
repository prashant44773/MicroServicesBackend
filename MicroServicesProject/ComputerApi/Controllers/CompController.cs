using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BooksService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompController : ControllerBase
    {
        // GET: api/<BooksController>
        [HttpGet("Get")]
        public IEnumerable<Comp> Get()
        {
            Comp dt = new Comp();
            List<Comp> lst = dt.GetCompList();
            return lst;
        }


        // POST api/<BooksController>
        [HttpPost("Post")]
        public IActionResult Post([FromBody] Comp data)
        {
            Comp book = new Comp();
            bool res = book.AddComp(data);

            if (res)
            {
                return Ok(new { StatusCode = 200 });
            }
            else
            {
                return Ok(new { StatusCode = 400 });
            }
        }

        // PUT api/<BooksController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BooksController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
