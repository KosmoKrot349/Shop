using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kr.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public double orderPrice { get; set; }
        public int countProduct { get; set; }
        public DateTime orderDate { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public bool isCompleet { get; set; }
    }
}