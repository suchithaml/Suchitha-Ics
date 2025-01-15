using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Trading_WebAPI.Models
{
    public class CheckOutRequest
    {
        public int UserId { get; set; }
        public CartItem[] Cart { get; set; }
        public int CustomerID { get; set; }
        public int VendorID { get; set; }
        public decimal OrderTotal { get; set; }
        public OrderDetailRequest[] OrderDetails { get; set; }
    }
}