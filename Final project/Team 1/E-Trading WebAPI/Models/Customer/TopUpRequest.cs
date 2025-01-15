using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Trading_WebAPI.Models
{
    public class TopUpRequest
    {
        public int UserID { get; set; }
        public decimal Amount { get; set; }
    }
}