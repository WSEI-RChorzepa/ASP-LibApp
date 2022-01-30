using System;
using System.ComponentModel.DataAnnotations;
using LibApp.Domain.Validations;

namespace LibApp.WebUI.Areas.Identity.Pages.Account
{
    public partial class EditModel
    {
       public class EditInputModel
        {
            [Required]
            public int Id { get; set; }

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
        }
    }
}
