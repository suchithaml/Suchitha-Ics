﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Trading_WebAPI.Models
{
    public class ProductDetailsDto
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string CategoryName { get; set; }
    }
}