using System;
using System.ComponentModel.DataAnnotations;

namespace LibApp.WebUI.Models
{
    public class AddOrUpdateBookFormModel
    {
        public int Id { get; set; }

        [Display(Name = "Nazwa")]
        [Required(ErrorMessage = "Pole '{0}' jest wymagane.")]
        [StringLength(255)]
        public string Name { get; set; }

        [Display(Name = "Autor")]
        [Required(ErrorMessage = "Pole '{0}' jest wymagane.")]
        public string AuthorName { get; set; }

        [Display(Name = "Gatunek")]
        [Required(ErrorMessage = "Pole '{0}' jest wymagane.")]
        public byte GenreId { get; set; }

        [Display(Name = "Data wydania")]
        [Required(ErrorMessage = "Pole '{0}' jest wymagane.")]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Ilość na stanie")]
        [Required(ErrorMessage = "Pole '{0}' jest wymagane.")]
        [Range(1, 20, ErrorMessage = "Ilość na stanie musi mieścić się w zakresie od {1} do {2}")]
        public int NumberInStock { get; set; }
    }
}
