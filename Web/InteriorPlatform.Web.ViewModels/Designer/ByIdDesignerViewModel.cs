namespace InteriorPlatform.Web.ViewModels.Designer
{
    using System;
    using System.Collections.Generic;

    using InteriorPlatform.Data.Models;
    using InteriorPlatform.Services.Mapping;
    using InteriorPlatform.Web.ViewModels.Project;

    public class ByIdDesignerViewModel : SingleDesignerViewModel, IMapFrom<ApplicationUser>
    {
        public DateTime CreatedOn { get; set; }

        public IEnumerable<SingleProjectViewModel> Projects { get; set; }
    }
}
