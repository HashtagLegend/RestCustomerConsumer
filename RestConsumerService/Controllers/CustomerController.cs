using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        public static int nextId = 0;

        private const string connectionString = "Data Source=(localdb)MSSQLLocalDB;Initial Catalog = CustomerDB; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


        private static List<Customer> cList = new List<Customer>()
        {
            new Customer("Patrick", "Ørum", 2018),
            new Customer("Frederik", "Wulff", 2017),
            new Customer("Hakan", "Aslan", 2016)
        };
        
        // GET: api/Customer
        [HttpGet]
        public List<Customer> Get()
        {
            var result = new List<Customer>();
            //Denne string er det sql query vi vil foretage
            string sql = "select id, firstname, lastname, year";

            //Laver en ny sql connection og giver connectionString som argument
            using (SqlConnection dbConnection = new SqlConnection(connectionString))
            {
                //Åbner forbindelsen til DB
                dbConnection.Open();

                //Det ehr er den sql command der skal udføres, den tager 2 parametre, sql query og connection
                using (SqlCommand selectCommand = new SqlCommand(sql, dbConnection))
                {
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        //Kører indtil der ikke er mere data at læse
                        while (reader.Read())
                        {
                            if (reader.HasRows)
                            {
                                //sætter int id = den value der står på index 0
                                int id = reader.GetInt32(0);
                                string firstname = reader.GetString(1);
                                string lastname = reader.GetString(2);
                                int year = reader.GetInt32(3);

                                var customer = new Customer()
                                {
                                    ID = id,
                                    FirstName = firstname,
                                    LastName =  lastname,
                                    Year = year
                                };

                                result.Add(customer);
                            }
                        }


                        
                    }
                }

            }
            //Returning from static list
            //return cList; 
            return result;
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
