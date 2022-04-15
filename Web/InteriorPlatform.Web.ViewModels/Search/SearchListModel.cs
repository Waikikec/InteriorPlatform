namespace InteriorPlatform.Web.ViewModels.Search
{
    using System.Collections.Generic;

    using InteriorPlatform.Web.ViewModels.Project;

    public class SearchListModel
    {
        public IEnumerable<SingleProjectViewModel> Projects { get; set; }
    }
}
