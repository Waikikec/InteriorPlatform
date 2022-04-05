namespace InteriorPlatform.Web.ViewModels.Image
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class CloudinaryImageInputModel
    {
        [Required]
        public IFormFile Image { get; set; }
    }
}
