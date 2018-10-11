using System;
using RestCustomerConsumer.Model;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace RestCustomerConsumer
{
    class Program
    {
        static string CustomersUri = "https://localhost:44325/api/customer";
        static void Main(string[] args)
        {
            System.Threading.Thread.Sleep(10000);

            foreach (var c in GetCustomersAsync().Result)    
            {
                Console.WriteLine(c);
            }

            
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




    }
}
