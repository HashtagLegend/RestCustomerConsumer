using System;
using RestCustomerConsumer.Model;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;


namespace RestCustomerConsumer
{
    class Program
    {

        //Jeg har tilføjet en kommentar
        static string CustomersUri = "https://localhost:5001/api/customer/";
        static void Main(string[] args)
        {
            System.Threading.Thread.Sleep(10000);

            foreach (var c in GetCustomersAsync().Result)    
            {
                Console.WriteLine(c);
            }

            Console.WriteLine("Get: " + GetCustomerByid(2).Result);

            Console.WriteLine("Post: " + PostCostumer(new Customer(4, "Theo", "Dyrman", 2016)).Result);
            
            Console.WriteLine("Put: " + PutCostumer(1, new Customer(5, "Fis", "Minister", 2016)).Result);

            Console.WriteLine("Get: " + GetCustomerByid(1).Result);

            Console.ReadKey();

        }



        public static async Task<IList<Customer>> GetCustomersAsync()
        {
            
            using (HttpClient client = new HttpClient())
            {
                string content = await client.GetStringAsync(CustomersUri);
                IList<Customer> cList = JsonConvert.DeserializeObject<IList<Customer>>(content);
                return cList;
            }
        }


        public static async Task<Customer> GetCustomerByid(int id)
        {
            using (HttpClient client = new HttpClient())
            {
               string content = await client.GetStringAsync(CustomersUri + id);
               Customer customer = JsonConvert.DeserializeObject<Customer>(content);
               return customer;
            }
        }

        public static async Task<string> PostCostumer(Customer customer)
        {
            using (HttpClient client = new HttpClient())
            {
                var jsonString = JsonConvert.SerializeObject(customer);
                StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(CustomersUri, content);
                
                return response.ToString();

            }
        }

        public static async Task<string> PutCostumer(int id, Customer customer)
        {
            using (HttpClient client = new HttpClient())
            {
                var jsonString = JsonConvert.SerializeObject(customer);
                StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(CustomersUri + id, content);

                return response.ToString();

            }
        }




    }
}
