namespace InteriorPlatform.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using InteriorPlatform.Data.Common.Repositories;
    using InteriorPlatform.Data.Models;
    using InteriorPlatform.Web.ViewModels.Project;

    public class ProjectsService : IProjectsService
    {
        private readonly IDeletableEntityRepository<Project> projectRepository;
        private readonly IDeletableEntityRepository<Style> styleRepository;

        public ProjectsService(
            IDeletableEntityRepository<Project> projectRepository,
            IDeletableEntityRepository<Style> styleRepository)
        {
            this.projectRepository = projectRepository;
            this.styleRepository = styleRepository;
        }

        public async Task CreateAsync(CreateProjectInputModel model, string userId, string imagePath)
        {
            var project = new Project
            {
                Name = model.Name,
                IsRealized = model.IsRealized,
                IsPublic = model.IsPublic,
                Description = model.Description,
                CategoryId = model.CategoryId,
                AddedByUserId = userId,
            };

            foreach (var style in model.Styles)
            {
                project.Styles.Add(style);
            }

            return;
        }
    }
}
