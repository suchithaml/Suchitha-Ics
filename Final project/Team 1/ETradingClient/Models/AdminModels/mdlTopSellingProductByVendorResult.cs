using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETradingClient.Models
{
    public class mdlTopSellingProductByVendorResult
    {
        public List<mdlReportProductDetails> mdlReportProductDetails { get; set; }
        public List<mdlGetVendorsTopSellingProductsByVendor> mdlGetVendorsTopSellingProductsByVendors { get; set; }
    }
}