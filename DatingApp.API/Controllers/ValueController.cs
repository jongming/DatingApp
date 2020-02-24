using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Controllers
{
    [Authorize] //This sets this class only to run with specific authroization
    [Route("api/[controller]")] //[controller] is just a place holder
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly DataContext _context;
        public ValuesController(DataContext context)
        {
            Console.WriteLine("***ValueController.cs: ValuesController() " + DateTime.Now.ToString());
            _context = context;

        }
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> GetValues() //IActionResult return HTTP 200 response
        {
            Console.WriteLine("***ValueController.cs: GetVAlues() " + DateTime.Now.ToString());
            var values = await _context.Values.ToListAsync();
            return Ok(values);
        }

        // GET api/values/5
        [AllowAnonymous] //this makes it not require authorization. **Just for testing
        [HttpGet("{id}")]
        public async Task<IActionResult> GetValue(int id)
        {
            Console.WriteLine("***ValueController.cs: GetValue() " + DateTime.Now.ToString());
            var value = await _context.Values.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(value);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
            Console.WriteLine("***ValueController.cs: Post() " + DateTime.Now.ToString());
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            Console.WriteLine("***ValueController.cs: Put() " + DateTime.Now.ToString());
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Console.WriteLine("***ValueController.cs: Delete() " + DateTime.Now.ToString());
        }
    }
}
