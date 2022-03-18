namespace InteriorPlatform.Web.ViewModels.Designer
{
    using InteriorPlatform.Data.Models;
    using InteriorPlatform.Services.Mapping;

    public class SingleDesignerViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ProfilePictureUrl { get; set; }

        public string PositionName { get; set; }

        public string TownName { get; set; }

        public int ProjectsCount { get; set; }
    }
}
