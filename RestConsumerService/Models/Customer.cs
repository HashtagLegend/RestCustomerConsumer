using RestConsumerService.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestConsumerService.Models
{
    public class Customer
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Year { get; set; }
        
        public Customer(string firstName, string lastName, int year)
        {
            CustomerController.nextId++;
            this.ID = CustomerController.nextId;
            FirstName = firstName;
            LastName = lastName;
            Year = year;
        }

        public Customer()
        {

        }

    }
}
