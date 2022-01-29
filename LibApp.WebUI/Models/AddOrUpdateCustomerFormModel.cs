using LibApp.Domain.Validations;
using System;
using System.ComponentModel.DataAnnotations;

namespace LibApp.WebUI.Models
{
    public class AddOrUpdateCustomerFormModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Please provide customer's name")]
        [StringLength(255)]
        public string Name { get; set; }

        public bool HasNewsletterSubscribed { get; set; }

        [Required(ErrorMessage = "Please select Membership Type")]
        [Display(Name = "Membership Type")]
        public byte? MembershipTypeId { get; set; }

        [Display(Name = "Date of Birth")]
        [Min18YearsIfMember]
        public DateTime? Birthdate { get; set; }
    }
}
