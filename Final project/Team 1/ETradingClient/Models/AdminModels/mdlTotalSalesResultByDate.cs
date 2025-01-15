using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ETradingClient.Custom_Validation;

namespace ETradingClient.Models
{
    public class mdlTotalSalesResultByDate
    {
        public decimal TotalSales { get; set; }
        [Required(ErrorMessage = "Sales Date is required.")]
        [PastDate(ErrorMessage = "Sales Date must be in the past.")]

        public DateTime SalesDate { get; set; }
    }
}