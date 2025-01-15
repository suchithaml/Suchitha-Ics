using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Trading_WebAPI.Models
{
    public class TopSellingProductByVendorResult
    {
        public int VendorID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int TotalQuantitySold { get; set; }
    }
}