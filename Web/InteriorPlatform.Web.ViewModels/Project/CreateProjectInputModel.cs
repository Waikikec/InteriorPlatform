namespace InteriorPlatform.Web.ViewModels.Project
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class CreateProjectInputModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        // [Display(Name = "Етап на проекта")]
        public bool IsRealized { get; set; }

        [Required]
        // [Display(Name = "Статус на проекта")]
        public bool IsPublic { get; set; }

        [Required]
        public string Description { get; set; }

        public IEnumerable<IFormFile> Images { get; set; }
    }
}
