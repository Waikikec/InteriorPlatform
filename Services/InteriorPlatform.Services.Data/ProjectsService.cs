namespace InteriorPlatform.Services.Data
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using InteriorPlatform.Data.Common.Repositories;
    using InteriorPlatform.Data.Models;
    using InteriorPlatform.Web.ViewModels.Project;

    public class ProjectsService : IProjectsService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };
        private readonly IDeletableEntityRepository<Project> projectsRepository;
        private readonly IDeletableEntityRepository<Style> stylesRepository;

        public ProjectsService(
            IDeletableEntityRepository<Project> projectsRepository,
            IDeletableEntityRepository<Style> stylesRepository)
        {
            this.projectsRepository = projectsRepository;
            this.stylesRepository = stylesRepository;
        }

        public async Task CreateAsync(CreateProjectInputModel model, string userId, string imagePath)
        {
            var project = new Project
            {
                Name = model.Name,
                Description = model.Description,
                Visits = 15,
                IsRealized = Convert.ToBoolean(model.IsRealized),
                IsPublic = Convert.ToBoolean(model.IsPublic),
                CategoryId = model.CategoryId,
                TownId = 1,
                CompanyId = 1,
                AddedByUserId = userId,
            };

            foreach (var styleId in model.Styles)
            {
                var currentStyle = this.stylesRepository.All().FirstOrDefault(x => x.Id == int.Parse(styleId));

                project.Styles.Add(currentStyle);
            }

            Directory.CreateDirectory($"{imagePath}/projects/");
            foreach (var image in model.Images)
            {
                var extension = Path.GetExtension(image.FileName).TrimStart('.');
                if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
                {
                    throw new System.Exception($"Invalid image extension {extension}");
                }

                var dbImage = new Image
                {
                    AddedByUserId = userId,
                    Extension = extension,
                };
                project.Images.Add(dbImage);

                var physicalPath = $"{imagePath}/projects/{dbImage.Id}.{extension}";
                using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
                await image.CopyToAsync(fileStream);
            }

            await this.projectsRepository.AddAsync(project);
            await this.projectsRepository.SaveChangesAsync();
        }
    }
}
