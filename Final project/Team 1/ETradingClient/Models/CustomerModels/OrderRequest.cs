using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace ETradingClient.Models
{
    public class OrderRequest
    {
        public int CustomerID { get; set; }
        public int VendorID { get; set; }
        public decimal OrderTotal { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}