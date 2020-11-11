using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Min18YearsIfAMember:ValidationAttribute
    {
        // override ValidationResult (option with 2 parameters)
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (Customer)validationContext.ObjectInstance; //get acces to object, cast it to customer

            if (customer.MembershipTypeId== MembershipType.Unknown 
                ||customer.MembershipTypeId == MembershipType.PayAsYouGo) //"magic number", need to refactor it
                return ValidationResult.Success;

            if (customer.Birthdate == null)
                return new ValidationResult("Birthday is required");

            var age = DateTime.Now.Year - customer.Birthdate.Value.Year; // not completly right, but out of scope for this exc

            return (age >= 18)
                ? ValidationResult.Success 
                : new ValidationResult("Customer needs to be at least 18 years to go on a membership");
         
        }
    }
}