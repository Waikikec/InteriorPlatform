namespace InteriorPlatform.Web.ViewModels.Project
{
    using System.Collections.Generic;
    using AutoMapper;
    using InteriorPlatform.Data.Models;
    using InteriorPlatform.Services.Mapping;

    public class ByIdProjectViewModel : IMapTo<Project>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public ICollection<Image> Images { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Project, ByIdProjectViewModel>()
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images));
        }
    }
}
