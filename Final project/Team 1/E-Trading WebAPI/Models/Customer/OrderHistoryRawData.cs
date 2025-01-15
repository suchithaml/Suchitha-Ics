using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Trading_WebAPI.Models
{
    public class OrderHistoryRawData
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal OrderTotal { get; set; }
        public string OrderStatus { get; set; }
        public string VendorName { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}