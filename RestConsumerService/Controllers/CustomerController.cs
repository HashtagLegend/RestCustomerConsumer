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

        private const string connectionString = "Server=tcp:custom3r.database.windows.net,1433;Initial Catalog=custom3rDB;Persist Security Info=False;User ID=hashtaglegend;Password=P@ssw0rd;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        private const string SelectAllCustomers = "select id, firstname, lastname, year from dbo.Customer";
        private const string InsertCustomer = "INSERT INTO dbo.Customer (firstname, lastname, year) VALUES (@firstname, @lastname, @year)";
        private const string deleteStatement = "DELETE FROM dbo.Customer where id=@id";
        private const string updateString ="UPDATE dbo.Customer set firstname=@firstname, lastname=@lastname, year=@year WHERE id=@id;";


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
            
            //Laver en ny sql connection og giver connectionString som argument
            using (SqlConnection dbConnection = new SqlConnection(connectionString))
            {
                //Åbner forbindelsen til DB
                dbConnection.Open();

                //Det her er den sql command der skal udføres, den tager 2 parametre, sql query og connection
                using (SqlCommand selectCommand = new SqlCommand(SelectAllCustomers, dbConnection))
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
                                    LastName = lastname,
                                    Year = year
                                };

                                result.Add(customer);
                            }
                        }
                    }
                }
            }
            //Returning from static list
            return result;
            //return cList;

        }


        // GET: api/Customer/5
        [HttpGet("{id}", Name = "Get")]
        //public IActionResult GetCustomer(int id)
        //{
        //    var cust = cList.FirstOrDefault((c) => c.ID == id);
        //    if(cust == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(cust);
        //}

        public Customer Get(int Id)
        {
            string sql = $"select id, firstname, lastname, year from dbo.Customer WHERE id = {Id}";
            
            using (SqlConnection dbConnection = new SqlConnection(connectionString))
            {
                dbConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(sql, dbConnection))
                {
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                string firstname = reader.GetString(1);
                                string lastname = reader.GetString(2);
                                int year = reader.GetInt32(3);

                                var customer = new Customer
                                {
                                    ID = id,
                                    FirstName = firstname,
                                    LastName = lastname,
                                    Year = year
                                };

                                return customer;
                            }
                        }
                    }
                }
            }
            //Returning from static list
            return null;
            //return cList;

        }


        //For at HttpResponseMessage fungere skal system.net.http tilføjes
        //For at bruge det i fiddler, skal der bare tilføjes en json body med alle properties.

        //POST: api/Customer
        [HttpPost]
        //public HttpResponseMessage PostCustomer(Customer c)
        //{
        //    cList.Add(c);
        //    return new HttpResponseMessage(System.Net.HttpStatusCode.Created);
        //}

        //Den nemme måde at lave en post på, dog uden responsemessages
        // POST: api/Customer
        //[HttpPost]
        //public void Post([FromBody] Customer c)
        //{
        //    cList.Add(c);
        //}


        [HttpPost]

        public int AddBook([FromBody] Customer cust)
        {
            using (SqlConnection databaseConnection = new SqlConnection(connectionString))
            {
                databaseConnection.Open();
                using (SqlCommand insertCommand = new SqlCommand(InsertCustomer, databaseConnection))
                {
                    insertCommand.Parameters.AddWithValue("@firstname", cust.FirstName);
                    insertCommand.Parameters.AddWithValue("@lastname", cust.LastName);
                    insertCommand.Parameters.AddWithValue("@year", cust.Year);
                    int rowsAffected = insertCommand.ExecuteNonQuery();
                    return rowsAffected;
                }
            }
        }

        //// PUT: api/Customer/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //PUT: api/Customer
        //[HttpPut]
        //public HttpResponseMessage PutCustomer(Customer cust)
        //{
        //    Customer customer = cList.Find(c => c.ID == cust.ID);

        //    if (customer == null)
        //    {
        //        return new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);
        //    }

        //    customer.ID = cust.ID;
        //    customer.FirstName = cust.FirstName;
        //    customer.LastName = cust.LastName;
        //    customer.Year = cust.Year;

        //    return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        //}

        [HttpPut("{id}")]

        public int UpdateCustomer(int id, [FromBody] Customer cust)
        {

            using (SqlConnection databaseConnection = new SqlConnection(connectionString))
            {
                databaseConnection.Open();
                using (SqlCommand updateCommand = new SqlCommand(updateString, databaseConnection))
                {
                    updateCommand.Parameters.AddWithValue("@firstname", cust.FirstName);
                    updateCommand.Parameters.AddWithValue("@lastname", cust.LastName);
                    updateCommand.Parameters.AddWithValue("@year", cust.Year);
                    updateCommand.Parameters.AddWithValue("@id", id);
                    int rowsAffected = updateCommand.ExecuteNonQuery();
                    return rowsAffected;
                }
            }
        }

        // DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}", Name = "Delete")]
        //public IEnumerable<Customer> DeleteCustomer(int id)
        //{
        //    Customer customer = cList.Find(c => c.ID == id);
        //    cList.Remove(customer);

        //    return cList;
        //}


        [HttpDelete("{id}")]

        public int DeleteCustomer(int id)
        {
            using (SqlConnection databaseConnection = new SqlConnection(connectionString))
            {
                databaseConnection.Open();
                using (SqlCommand insertCommand = new SqlCommand(deleteStatement, databaseConnection))
                {
                    insertCommand.Parameters.AddWithValue("@id", id);                
                    int rowsAffected = insertCommand.ExecuteNonQuery();
                    return rowsAffected;

                }
            }
        }
    }
}
