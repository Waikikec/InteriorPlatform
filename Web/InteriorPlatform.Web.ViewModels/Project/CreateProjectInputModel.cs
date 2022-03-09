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
        public bool IsRealized { get; set; }

        [Required]
        public bool IsPublic { get; set; }

        [Required]
        public string Description { get; set; }

        public int CategoryId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> CategoriesItems { get; set; }

        public int StyleId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> StylesItems { get; set; }

        public IEnumerable<IFormFile> Images { get; set; }
    }
}
