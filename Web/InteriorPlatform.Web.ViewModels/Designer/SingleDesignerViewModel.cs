namespace InteriorPlatform.Web.ViewModels.Designer
{
    using System;

    using AutoMapper;
    using InteriorPlatform.Data.Models;
    using InteriorPlatform.Services.Mapping;

    public class SingleDesignerViewModel : IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public CloudImage ProfilePicture { get; set; }

        public string PositionName { get; set; }

        public string TownName { get; set; }

        public string AboutMe { get; set; }

        public int ProjectsCount { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ApplicationUser, SingleDesignerViewModel>()
                .ForMember(dest => dest.ProjectsCount, opt => opt.MapFrom(src => src.UserProjects.Count));
        }
    }
}
