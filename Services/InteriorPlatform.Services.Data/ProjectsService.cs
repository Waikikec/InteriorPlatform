namespace InteriorPlatform.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using InteriorPlatform.Data.Common.Repositories;
    using InteriorPlatform.Data.Models;
    using InteriorPlatform.Services.Mapping;
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

        public async Task CreateAsync(CreateProjectInputModel model, ApplicationUser user, string imagePath)
        {
            var project = new Project
            {
                Name = model.Name,
                Description = model.Description,
                IsRealized = Convert.ToBoolean(model.IsRealized),
                IsPublic = Convert.ToBoolean(model.IsPublic),
                CategoryId = model.CategoryId,
                TownId = user.TownId,
                CompanyId = user.CompanyId,
                AddedByUserId = user.Id,
            };

            foreach (var styleId in model.Styles)
            {
                var currentStyle = this.stylesRepository
                    .All()
                    .FirstOrDefault(x => x.Id == int.Parse(styleId));

                project.Styles.Add(currentStyle);
            }

            Directory.CreateDirectory($"{imagePath}/images/projects/");
            foreach (var image in model.Images)
            {
                var extension = Path.GetExtension(image.FileName).TrimStart('.');
                if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
                {
                    throw new System.Exception($"Invalid image extension {extension}");
                }

                var dbImage = new Image
                {
                    AddedByUserId = user.Id,
                    Extension = extension,
                };
                project.Images.Add(dbImage);

                var physicalPath = $"{imagePath}/images/projects/{dbImage.Id}.{extension}";
                using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
                await image.CopyToAsync(fileStream);
            }

            await this.projectsRepository.AddAsync(project);
            await this.projectsRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var project = this.projectsRepository
                .All()
                .FirstOrDefault(x => x.Id == id);

            this.projectsRepository.Delete(project);
            await this.projectsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 6)
        {
            var projects = this.projectsRepository
                .AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<T>()
                .ToList();

            return projects;
        }

        public IEnumerable<T> GetAllByUserId<T>(string id)
        {
            var projects = this.projectsRepository
                .AllAsNoTracking()
                .Where(x => x.AddedByUserId == id)
                .To<T>()
                .ToList();

            return projects;
        }

        public T GetById<T>(int id)
        {
            var project = this.projectsRepository
                .AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();

            return project;
        }

        public int GetCount()
        {
            return this.projectsRepository
                .AllAsNoTracking()
                .Count();
        }

        public IEnumerable<T> GetRandom<T>(int count)
        {
            return this.projectsRepository
                .AllAsNoTracking()
                .OrderBy(x => Guid.NewGuid())
                .Take(count)
                .To<T>()
                .ToList();
        }

        public async Task UpdateAsync(int id, EditProjectInputModel input)
        {
            var project = this.projectsRepository
                .Get(null, null, "Styles")
                .FirstOrDefault(x => x.Id == id);

            project.Name = input.Name;
            project.IsRealized = Convert.ToBoolean(input.IsRealized);
            project.IsPublic = Convert.ToBoolean(input.IsPublic);
            project.Description = input.Description;
            project.CategoryId = input.CategoryId;

            project.Styles.Clear();

            foreach (var styleId in input.Styles)
            {
                var currentStyle = this.stylesRepository
                    .All()
                    .FirstOrDefault(x => x.Id == styleId);

                project.Styles.Add(currentStyle);
            }

            await this.projectsRepository.SaveChangesAsync();
        }
    }
}
