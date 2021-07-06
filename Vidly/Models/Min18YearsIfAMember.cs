using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Min18YearsIfAMember:ValidationAttribute
    {
        //any membership type other than pay as you go will required 18 years of age
        //here we override isValid() method

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //we will apply [Min18YearsIfAMember] attribute on the model property
            // ValidationContext.ObjectInstance this gives us the containing class on which this validationattribute is applied
            var customer = (Customer)validationContext.ObjectInstance;
            if(customer.MembershipTypeID == 0 || customer.MembershipTypeID==1)//0 means we do not selected any membership type and for that no error should come
            {
                //means pay as you go customer
                return ValidationResult.Success;
            }
            if(customer.BirthDate==null)
            {
                return new ValidationResult("Birthdate is required");
            }
            var age = DateTime.Today.Year - customer.BirthDate.Value.Year;
            return (age >= 18) ? ValidationResult.Success : new ValidationResult("Customer should be 18 year of age to go on membership ");
        }
       
    }
}