using LibApp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace LibApp.Domain.Validations
{
    public class Min18YearsIfMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var instance = validationContext.ObjectInstance;
            var type = instance.GetType();

            if (type.GetProperty("MembershipTypeId").GetValue(instance) == null)
                return ValidationResult.Success;

            var membershipTypeId = (byte)type.GetProperty("MembershipTypeId").GetValue(instance);
            var birthdate = (Nullable<DateTime>)type.GetProperty("Birthdate").GetValue(instance);

            if (membershipTypeId == MembershipType.Unknown ||
                membershipTypeId == MembershipType.PayAsYouGo)
            {
                return ValidationResult.Success;
            }

            if (birthdate == null)
            {
                return new ValidationResult("Birthdate is required");
            }

            var age = DateTime.Today.Year - birthdate.Value.Year;

            return age >= 18
                ? ValidationResult.Success : new ValidationResult("Customer shoudl be at least 18 years old to subscribe");
        }
    }
}
