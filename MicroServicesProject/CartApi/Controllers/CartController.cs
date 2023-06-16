using CartApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CartApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {

        // GET: api/<CartController>
        [HttpGet("UserCartCount")]
        public int UserCartCount([FromQuery(Name = "UserID")] int UserID)
        {
            Cart cart = new Cart();
            int res = cart.GetCartCount(UserID);
            return res;
        }

        // GET: api/<CartController>
        [HttpGet("UserCart")]
        public IEnumerable<Products> UserCart([FromQuery(Name = "UserID")]int UserID)
        { 
            Cart cart = new Cart();
            List<Products> lst = cart.GetCartList(UserID);
            return lst;
        }


        // POST api/<CartController>
        [HttpPost("BookCart")]
        public IActionResult BookCart([FromBody] Products add)
        {
            Cart cart = new Cart();
            bool res = cart.AddToBookCart(add);

            if (res)
            {
                return Ok(new { StatusCode = 200 });
            }
            else
            {
                return Ok(new { StatusCode = 400 });
            }
        }


        // POST api/<CartController>
        [HttpPost("CompCart")]
        public IActionResult CompCart([FromBody] Products add)
        {
            Cart cart = new Cart();
            bool res = cart.AddToCompCart(add);

            if (res)
            {
                return Ok(new { StatusCode = 200 });
            }
            else
            {
                return Ok(new { StatusCode = 400 });
            }
        }


        // POST api/<CartController>
        [HttpPost("FashCart")]
        public IActionResult FashCart([FromBody] Products add)
        {
            Cart cart = new Cart();
            bool res = cart.AddToFashCart(add);

            if (res)
            {
                return Ok(new { StatusCode = 200 });
            }
            else
            {
                return Ok(new { StatusCode = 400 });
            }
        }


        // PUT api/<CartController>/5
        [HttpPut("Quantity")]
        public IActionResult Quantity([FromBody] Products Quan)
        {
            // If The User Tries To Add 0 as a Quantity
            if (Quan.Quantity == 0)
            {
                return Ok(new { StatusCode = 400 });
            }


            Cart cart = new Cart();
            bool res = cart.UpdateQuantity(Quan);

            if (res)
            {
                return Ok(new { StatusCode = 200 });
            }
            else
            {
                return Ok(new { StatusCode = 400 });
            }
        }


        // PUT api/<CartController>/5
        [HttpPut("Remove")]
        public IActionResult Remove([FromBody] Products Quan)
        {
            Cart cart = new Cart();
            bool res = cart.RemoveFromCart(Quan);

            if (res)
            {
                return Ok(new { StatusCode = 200 });
            }
            else
            {
                return Ok(new { StatusCode = 400 });
            }
        }

        // GET: api/<CartController>
        [HttpGet("isActive")]
        public bool isActive()
        {
            bool res = true;
            return res;
        }

    }
}
