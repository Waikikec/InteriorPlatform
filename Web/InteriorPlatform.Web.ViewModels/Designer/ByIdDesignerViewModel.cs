﻿namespace InteriorPlatform.Web.ViewModels.Designer
{
    using System.Collections.Generic;

    using InteriorPlatform.Web.ViewModels.Project;

    public class ByIdDesignerViewModel
    {
        public SingleDesignerViewModel Designer { get; set; }

        public IEnumerable<SingleProjectViewModel> Projects { get; set; }
    }
}
