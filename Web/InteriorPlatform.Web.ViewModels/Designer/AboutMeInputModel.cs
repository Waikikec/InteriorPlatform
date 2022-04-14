namespace InteriorPlatform.Web.ViewModels.Designer
{
    using System.ComponentModel.DataAnnotations;

    public class AboutMeInputModel
    {
        [Required(ErrorMessage = "Полето '{0}' е задължително.")]
        [Display(Name = "Описание")]
        [StringLength(500, ErrorMessage = "Полето '{0}' трябва да бъде между {2} и максимум {1} символа.", MinimumLength = 1)]
        public string Info { get; set; }
    }
}
