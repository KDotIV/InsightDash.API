using System;

namespace InsightDash.API.Models
{
    public class Order
    {
        public int id { get; set; }
        public Customer Customer { get; set; }
        public decimal orderTotal { get; set; }
        public DateTime placed { get; set; }
        public DateTime? Completed { get; set; }
        
    }
}