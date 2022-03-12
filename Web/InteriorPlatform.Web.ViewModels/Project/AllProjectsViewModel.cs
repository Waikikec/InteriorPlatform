namespace InteriorPlatform.Web.ViewModels.Project
{
    using System.Collections.Generic;

    public class AllProjectsViewModel
    {
        public IEnumerable<SingleProjectViewModel> Projects { get; set; }
    }
}
