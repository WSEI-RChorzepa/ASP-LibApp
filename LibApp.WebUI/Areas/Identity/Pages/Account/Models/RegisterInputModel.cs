using System;
using System.ComponentModel.DataAnnotations;
using LibApp.Domain.Validations;

namespace LibApp.WebUI.Areas.Identity.Pages.Account
{
    public partial class RegisterModel
    {
        public class RegisterInputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Please provide customer's name")]
            [StringLength(255)]
            public string Name { get; set; }

            [Display(Name = "Has newsletter subscribed")]
            public bool HasNewsletterSubscribed { get; set; }

            [Display(Name = "Membership Type")]
            public byte MembershipTypeId { get; set; }

            [Display(Name = "Date of Birth")]
            [Min18YearsIfMember]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            [DataType(DataType.Date)]
            public DateTime? Birthdate { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }
    }
}
