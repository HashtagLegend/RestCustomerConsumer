using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestConsumerService.Models;

namespace RestConsumerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private static List<Customer> cList = new List<Customer>()
        {
            new Customer(1, "Patrick", "Ørum", 2018),
            new Customer(2, "Frederik", "Wulff", 2017),
            new Customer(3, "Hakan", "Aslan", 2016)
        };
        
        

        // GET: api/Customer

        [HttpGet]
        public List<Customer> Get()
        {
            return cList; 
        }


        // GET: api/Customer/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Customer
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Customer/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
