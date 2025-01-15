using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETradingClient.Models
{
    public class Product
    {
        public int VendorID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public long Stock { get; set; }
        public string ImagePath { get; set; }
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }
        public List<Category> listCatgories { get; set; }
    }
}