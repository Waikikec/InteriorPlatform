namespace InteriorPlatform.Web.ViewModels.Designer
{
    using System.ComponentModel.DataAnnotations;

    using InteriorPlatform.Data.Models;
    using InteriorPlatform.Services.Mapping;

    public class AboutMeInputModel : IMapTo<ApplicationUser>
    {
        [Required(ErrorMessage = "Полето '{0}' е задължително.")]
        [Display(Name = "Описание")]
        [StringLength(500, ErrorMessage = "Полето '{0}' трябва да бъде между {2} и максимум {1} символа.", MinimumLength = 1)]
        public string AboutMe { get; set; }
    }
}
