using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETradingClient.Models
{
    public class mdlReportProductDetails
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int TotalQuantitySold { get; set; }
    }
}