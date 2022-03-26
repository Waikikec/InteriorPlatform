namespace InteriorPlatform.Web.ViewModels.Home
{
    using System.Collections.Generic;

    using InteriorPlatform.Web.ViewModels.Designer;
    using InteriorPlatform.Web.ViewModels.Project;

    public class HomePageViewModel
    {
        public IEnumerable<SingleProjectViewModel> RandomProjects { get; set; }

        public IEnumerable<SingleDesignerViewModel> RandomDesigners { get; set; }
    }
}
