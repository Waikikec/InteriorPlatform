namespace InteriorPlatform.Web.ViewModels.Designer
{
    using System.Collections.Generic;

    using InteriorPlatform.Web.ViewModels.Image;
    using InteriorPlatform.Web.ViewModels.Inquire;
    using InteriorPlatform.Web.ViewModels.Project;

    public class ByIdDesignerViewModel
    {
        public CloudinaryImageInputModel CloudinaryImage { get; set; }

        public SingleDesignerViewModel Designer { get; set; }

        public IEnumerable<SingleProjectViewModel> Projects { get; set; }

        public IEnumerable<InquireViewModel> Inquires { get; set; }
    }
}
