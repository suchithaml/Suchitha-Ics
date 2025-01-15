using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETradingClient.Custom_Validation
{
    public class PastDateAttribute: ValidationAttribute
    {
        public PastDateAttribute() : base("The date must be in the past.") { }

        public override bool IsValid(object value)
        {
            if (value is DateTime dateTime)
            {
                return dateTime < DateTime.Now;
            }
            return false;
        }
    }
}