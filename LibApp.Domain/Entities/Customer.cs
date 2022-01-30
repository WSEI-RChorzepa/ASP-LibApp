using LibApp.Domain.Validations;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace LibApp.Domain.Entities
{
    public class Customer : IdentityUser<int>
    {
        [Required(ErrorMessage = "Please provide customer's name")]
        [StringLength(255)]
        [PersonalData]
        public string Name { get; set; }

        [PersonalData]
        public bool HasNewsletterSubscribed { get; set; }

        [PersonalData]
        public MembershipType MembershipType { get; set; }

        [Display(Name = "Membership Type")]
        [PersonalData]
        public byte MembershipTypeId { get; set; }

        [PersonalData]
        [Display(Name = "Date of Birth")]
        [Min18YearsIfMember]
        public DateTime? Birthdate { get; set; }
    }
}