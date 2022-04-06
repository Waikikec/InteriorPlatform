namespace InteriorPlatform.Web.ViewModels.Inquire
{
    using InteriorPlatform.Web.ViewModels.Designer;
    using InteriorPlatform.Web.ViewModels.Project;

    public class InquireAssemblyViewModel
    {
        public InquireInputModel Inquire { get; set; }

        public ByIdProjectViewModel Project { get; set; }

        public SingleDesignerViewModel Designer { get; set; }
    }
}
