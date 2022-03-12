namespace InteriorPlatform.Web.ViewModels.Project
{
    using System.Linq;

    using AutoMapper;
    using InteriorPlatform.Data.Models;
    using InteriorPlatform.Services.Mapping;

    public class SingleProjectViewModel : IMapFrom<Project>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string CategoryName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Project, SingleProjectViewModel>()
                .ForMember(x => x.ImageUrl, opt => opt.MapFrom(x =>
                    x.Images.FirstOrDefault().RemoteImageUrl ?? "/images/projects/" + x.Images.FirstOrDefault().Id + "." + x.Images.FirstOrDefault().Extension));
        }
    }
}
