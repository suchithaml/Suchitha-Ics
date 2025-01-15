using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Trading_WebAPI.Models
{
    public class GetVendorsTopSellingProductsByVendor
    {
        public int VendorID { get; set; }
        public string VendorName { get; set; }
    }
}