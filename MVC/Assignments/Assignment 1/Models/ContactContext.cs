using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Mvc_Assignment_1.Models
{
    public class ContactContext :DbContext
    {
        public ContactContext():base("name= connectstr") { }
        public DbSet<Contact> contact { get; set; }
    }
}