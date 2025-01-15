using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETradingClient.Models
{
    public class Vendor
    {
        public int VendorID { get; set; }
        public string VendorName { get; set; }
        public string VendorCompanyPhoneNo { get; set; }
        public string VendorCompanyAddress { get; set; }
        public string VendorCompanyEmailID { get; set; }
        public string VendorCompanyName { get; set; }
        public bool VendorIsActive { get; set; }
        public int CustomerID { get; set; }
    }
}