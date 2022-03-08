namespace InteriorPlatform.Services.Data
{
    using System;
    using System.Threading.Tasks;
    using InteriorPlatform.Data.Common.Repositories;
    using InteriorPlatform.Data.Models;
    using InteriorPlatform.Web.ViewModels.Project;

    public class ProjectsService : IProjectsService
    {
        private readonly IDeletableEntityRepository<Project> projectRepository;

        public ProjectsService(
            IDeletableEntityRepository<Project> projectRepository)
        {
            this.projectRepository = projectRepository;
        }

        public Task CreateAsync(CreateProjectInputModel model, string userId, string imagePath)
        {
            //var project = new Project
            //{
            //    Name = model.Name,
            //    Description = model.Description,
            //    IsRealized = model.IsRealized,
            //    IsPublic = model.IsPublic,                
            //    CategoryId = model.CategoryId,

            //    AddedByUserId = userId,
            //}
            return null;
        }
    }
}
