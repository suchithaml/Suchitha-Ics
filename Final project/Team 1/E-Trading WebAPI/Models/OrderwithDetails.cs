using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Trading_WebAPI.Models
{
    public class OrderwithDetails
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public int VendorID { get; set; }
        public decimal OrderTotal { get; set; }
        public string OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }

        // Order details
        public int OrderDetailID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}