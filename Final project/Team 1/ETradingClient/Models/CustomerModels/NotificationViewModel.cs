using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETradingClient.Models
{
    public class NotificationViewModel
    {
        public int NotificationID { get; set; }
        public string ProductName { get; set; }
        public decimal OldPrice { get; set; }
        public decimal NewPrice { get; set; }
        public string UpdatedAt { get; set; } // Format this as a string if desired
    }
}