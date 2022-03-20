namespace InteriorPlatform.Web.ViewModels.Inquire
{
    using System.ComponentModel.DataAnnotations;

    using InteriorPlatform.Data.Models;

    public class InquireInputModel
    {
        [Required(ErrorMessage = "Полето '{0}' е задължително.")]
        [Display(Name = "Име")]
        [StringLength(30, ErrorMessage = "Полето '{0}' трябва да бъде между {2} и максимум {1} символа.", MinimumLength = 1)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Полето '{0}' е задължително.")]
        [Display(Name = "Телефон")]
        [StringLength(30, ErrorMessage = "Полето '{0}' трябва да бъде между {2} и максимум {1} символа.", MinimumLength = 1)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Полето '{0}' е задължително.")]
        [Display(Name = "Имейл")]
        [StringLength(30, ErrorMessage = "Полето '{0}' трябва да бъде между {2} и максимум {1} символа.", MinimumLength = 1)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Полето '{0}' е задължително.")]
        [Display(Name = "Описание")]
        [StringLength(500, ErrorMessage = "Полето '{0}' трябва да бъде между {2} и максимум {1} символа.", MinimumLength = 1)]
        public string Info { get; set; }

        public ApplicationUser AddedByUserId { get; set; }
    }
}
