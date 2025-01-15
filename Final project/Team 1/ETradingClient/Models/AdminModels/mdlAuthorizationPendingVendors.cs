using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ETradingClient.Models
{
    public class mdlAuthorizationPendingVendors
    {
        [DisplayName("ID")]
        public int VendorID { get; set; }

        [DisplayName("Name")]
        public string VendorName { get; set; }

        [DisplayName("Phone No.")]
        public string VendorCompanyPhoneNo { get; set; }

        [DisplayName("Address")]
        public string VendorCompanyAddress { get; set; }

        [DisplayName("Mail ID")]
        public string VendorCompanyEmailID { get; set; }

        [DisplayName("Company Name")]
        public string VendorCompanyName { get; set; }

        public int UserID { get; set; }

        public string Username { get; set; }
        public bool IsVendor { get; set; }
        public string Status { get; set; }
    }
}