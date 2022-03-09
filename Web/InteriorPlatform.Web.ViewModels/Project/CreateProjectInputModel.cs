namespace InteriorPlatform.Web.ViewModels.Project
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class CreateProjectInputModel
    {
        [Required(ErrorMessage = "Полето '{0}' е задължително.")]
        [StringLength(30, ErrorMessage = "Полето '{0}' трябва да бъде между {2} и максимум {1} символа.", MinimumLength = 3)]
        public string Name { get; set; }

        //[Required(ErrorMessage = "Полето '{0}' е задължително.")]
        //[Range(typeof(bool), "true", "true", ErrorMessage = "Полето '{0}' трябва да бъде между {2} и максимум {1} символа.")]
        public bool IsRealized { get; set; }

        //[Required(ErrorMessage = "Полето '{0}' е задължително.")]
        //[Range(typeof(bool), "true", "true", ErrorMessage = "Полето '{0}' трябва да бъде между {2} и максимум {1} символа.")]
        public bool IsPublic { get; set; }

        [Required(ErrorMessage = "Полето '{0}' е задължително.")]
        [StringLength(30, ErrorMessage = "Полето '{0}' трябва да бъде между {2} и максимум {1} символа.", MinimumLength = 3)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Полето '{0}' е задължително.")]
        public int CategoryId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> CategoriesItems { get; set; }

        public IEnumerable<string> Styles { get; set; }

        public IEnumerable<KeyValuePair<string, string>> StylesItems { get; set; }

        public IEnumerable<IFormFile> Images { get; set; }
    }
}
