using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestConsumerService.Models;
using System.Web;
using System.Net.Http;

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
        public IActionResult GetCustomer(int id)
        {
            var cust = cList.FirstOrDefault((c) => c.ID == id);
            if(cust == null)
            {
                return NotFound();
            }

            return Ok(cust);
        }

        //For at HttpResponseMessage fungere system.net.http tilføjes
        //For at bruge det i fiddler, skal der bare tilføjes en json body med alle properties.
        //POST: api/Customer
        [HttpPost]
        public HttpResponseMessage PostCustomer(Customer c)
        {
            cList.Add(c);
            return new HttpResponseMessage(System.Net.HttpStatusCode.Created);
        }

        //Den nemme måde at lave en post på, dog uden responsemessages
        // POST: api/Customer
        //[HttpPost]
        //public void Post([FromBody] Customer c)
        //{
        //    cList.Add(c);
        //}

        //// PUT: api/Customer/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //PUT: api/Customer
        [HttpPut]
        public HttpResponseMessage PutCustomer(Customer cust)
        {
            Customer customer = cList.Find(c => c.ID == cust.ID);
            
            if (customer == null)
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);
            }

            customer.ID = cust.ID;
            customer.FirstName = cust.FirstName;
            customer.LastName = cust.LastName;
            customer.Year = cust.Year;

            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }



        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}", Name = "Delete")]
        public IEnumerable<Customer> DeleteCustomer(int id)
        {
            Customer customer = cList.Find(c => c.ID == id);
            cList.Remove(customer);

            return cList;
        }

    }
}
