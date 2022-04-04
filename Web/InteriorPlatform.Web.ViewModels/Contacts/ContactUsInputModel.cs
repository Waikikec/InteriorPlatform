namespace InteriorPlatform.Web.ViewModels.Contacts
{
    using System.ComponentModel.DataAnnotations;

    public class ContactUsInputModel
    {
        [Required(ErrorMessage = "Полето '{0}' е задължително.")]
        [Display(Name = "Име")]
        [StringLength(30, ErrorMessage = "Полето '{0}' трябва да бъде между {2} и максимум {1} символа.", MinimumLength = 1)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Полето '{0}' е задължително.")]
        [Display(Name = "Фамилия")]
        [StringLength(30, ErrorMessage = "Полето '{0}' трябва да бъде между {2} и максимум {1} символа.", MinimumLength = 1)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Полето '{0}' е задължително.")]
        [Display(Name = "Имейл")]
        [EmailAddress]
        [StringLength(30, ErrorMessage = "Полето '{0}' трябва да бъде между {2} и максимум {1} символа.", MinimumLength = 1)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Полето '{0}' е задължително.")]
        [Display(Name = "Относно")]
        [StringLength(1000, ErrorMessage = "Полето '{0}' трябва да бъде между {2} и максимум {1} символа.", MinimumLength = 1)]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Полето '{0}' е задължително.")]
        [Display(Name = "Съобщение")]
        [StringLength(1000, ErrorMessage = "Полето '{0}' трябва да бъде между {2} и максимум {1} символа.", MinimumLength = 1)]
        public string Message { get; set; }
    }
}
