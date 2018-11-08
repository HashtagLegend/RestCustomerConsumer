using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestConsumerService.Models;

namespace RestConsumerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private const string connectionString = "Server=tcp:custom3r.database.windows.net,1433;Initial Catalog=custom3rDB;Persist Security Info=False;User ID=hashtaglegend;Password=P@ssw0rd;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        private const string SelectAllCustomers = "SELECT c.id, c.firstname, c.lastname, o.OrderId FROM dbo.Customers AS c, dbo.Order AS o WHERE o.KundeId = c.id";

        // GET: api/Order
        [HttpGet]
        public List<Order> Get()
        {
            var result = new List<Customer>();

            using (SqlConnection dbConnection = new SqlConnection(connectionString))
            {
             dbConnection.Open();

                using (SqlCommand selectCommand = new SqlCommand(SelectAllCustomers, dbConnection))
                {
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader.HasRows)
                            {
                                int kundeId = reader.GetInt32(0);
                                string firstname = reader.GetString(1);
                                string lastname = reader.GetString(2);
                                int orderID = reader.GetInt32(3);

                                var customer = new Order()
                                {
                                    KundeId = kundeId,
                                    FirstName = firstname,
                                    LastName = lastname
                                    
                                };

                                result.Add(customer);
                            }
                        }
                    }
                }
            }
            return result;
         
        }

        // GET: api/Order/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Order
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Order/5
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
