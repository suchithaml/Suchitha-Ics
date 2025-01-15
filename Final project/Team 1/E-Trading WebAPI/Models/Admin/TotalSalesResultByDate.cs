using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Trading_WebAPI.Models
{
    public class TotalSalesResultByDate
    {
        public decimal TotalSales { get; set; }
        public DateTime SalesDate { get; set; }
    }
}