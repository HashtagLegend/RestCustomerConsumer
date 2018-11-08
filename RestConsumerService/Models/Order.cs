using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestConsumerService.Models
{
    public class Order
    {
        public int KundeId { get; set; }
        public int OrderId { get; set; }
        public DateTime Date { get; set; }

        public Order(int kundeId, int orderId, DateTime date)
        {
            KundeId = kundeId;
            OrderId = orderId;
            Date = date;
        }

        public Order()
        {
            
        }
    }
}
