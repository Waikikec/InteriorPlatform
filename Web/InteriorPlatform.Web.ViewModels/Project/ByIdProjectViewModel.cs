namespace InteriorPlatform.Web.ViewModels.Project
{
    using System;
    using System.Collections.Generic;

    using AutoMapper;
    using InteriorPlatform.Data.Models;
    using InteriorPlatform.Services.Mapping;

    public class ByIdProjectViewModel : IMapTo<Project>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public Category Category { get; set; }

        public Company Company { get; set; }

        public ApplicationUser AddedByUser { get; set; }

        public ICollection<Image> Images { get; set; }

        public ICollection<Like> Likes { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Project, ByIdProjectViewModel>()
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images))
                .ForMember(dest => dest.Likes, opt => opt.MapFrom(src => src.Likes));
        }
    }
}
