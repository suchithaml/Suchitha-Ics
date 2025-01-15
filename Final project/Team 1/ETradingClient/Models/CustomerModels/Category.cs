using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ETradingClient.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        [Required(ErrorMessage = "Category Name is required.")]
        [StringLength(100, ErrorMessage = "Category Name can't be longer than 100 characters.")]
        [MinLength(3, ErrorMessage = "Category Name atleast characters should contain.")]
        public string CategoryName { get; set; }
    }
}