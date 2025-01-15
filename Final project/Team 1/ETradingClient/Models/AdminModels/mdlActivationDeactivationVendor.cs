using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ETradingClient.Models
{
    public class mdlActivationDeactivationVendor
    {
        [DisplayName("ID")]
        public int VendorID { get; set; }

        [DisplayName("Name")]
        [Required(ErrorMessage = "Vendor name is required.")]
        [StringLength(30, ErrorMessage = "Vendor name cannot be longer than 30 characters.")]
        public string VendorName { get; set; }

        [DisplayName("Phone No.")]
        [StringLength(10, ErrorMessage = "Phone number must be 10 characters long.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be numeric and 10 digits long.")]
        public string VendorCompanyPhoneNo { get; set; }

        [DisplayName("Address")]
        [StringLength(45, ErrorMessage = "Address cannot be longer than 45 characters.")]
        public string VendorCompanyAddress { get; set; }

        [DisplayName("Mail ID")]
        [StringLength(45, ErrorMessage = "Email address cannot be longer than 45 characters.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string VendorCompanyEmailID { get; set; }

        [DisplayName("Company Name")]
        [Required(ErrorMessage = "Company name is required.")]
        [StringLength(45, ErrorMessage = "Company name cannot be longer than 45 characters.")]
        public string VendorCompanyName { get; set; }

        public bool VendorIsActive { get; set; }

        public int CustomerID { get; set; }
    
    }
}