namespace InteriorPlatform.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using InteriorPlatform.Data;
    using InteriorPlatform.Data.Common.Repositories;
    using InteriorPlatform.Data.Models;
    using InteriorPlatform.Data.Repositories;
    using InteriorPlatform.Services.Mapping;
    using InteriorPlatform.Web.ViewModels.Project;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class ProjectsServiceTests : IDisposable
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IProjectsService projectsService;
        private readonly EfDeletableEntityRepository<Project> projectsRepository;
        private readonly EfDeletableEntityRepository<Style> stylesRepository;
        private readonly IRepository<UserProject> userProjectsRepository;

        public ProjectsServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            this.dbContext = new ApplicationDbContext(options);

            this.projectsRepository = new EfDeletableEntityRepository<Project>(this.dbContext);
            this.stylesRepository = new EfDeletableEntityRepository<Style>(this.dbContext);

            this.projectsService = new ProjectsService(this.projectsRepository, this.stylesRepository, this.userProjectsRepository);

            AutoMapperConfig.RegisterMappings(typeof(SingleProjectViewModel).Assembly, typeof(Project).Assembly);
        }

        [Fact] // public async Task CreateAsync(CreateProjectInputModel model, ApplicationUser user, string imagePath)
        public async Task CreateProjectAsyncSuccessfully()
        {
            await this.SeedDatabase();

            Assert.Equal(1, this.projectsRepository.All().Count());
        }

        [Fact] // public async Task DeleteAsync(int id)
        public async Task DeleteProjectAsyncSuccessfully()
        {
            await this.SeedDatabase();

            var projectId = this.projectsRepository.All().FirstOrDefault().Id;

            await this.projectsService.DeleteAsync(projectId);

            var count = await this.projectsRepository.All().CountAsync();

            Assert.Equal(0, count);
        }

        [Fact]
        public async Task DeleteProjectAsyncThrowException()
        {
            await this.SeedDatabase();

            var exception = await Assert
                .ThrowsAsync<NullReferenceException>(async () => await this.projectsService.DeleteAsync(3));

            Assert.Equal("Project is not found!", exception.Message);
        }

        [Fact] // public async Task UpdateAsync(int id, EditProjectInputModel input)
        public async Task UpdateProjectAsyncSuccessfully()
        {
            await this.SeedDatabase();

            var project = this.projectsRepository.All().FirstOrDefault();

            var style = new Style { Id = 2, Name = "Модерен" };
            await this.stylesRepository.AddAsync(style);
            await this.stylesRepository.SaveChangesAsync();

            var model = new EditProjectInputModel()
            {
                Name = "Edited Project Name",
                Description = "Edited Description",
                Styles = new List<int> { 2 },
            };

            await this.projectsService.UpdateAsync(project.Id, model);

            Assert.Equal(model.Name, project.Name);
            Assert.Equal(model.Description, project.Description);
            Assert.Equal(style, project.Styles.FirstOrDefault());
        }

        [Fact]
        public async Task UpdateProjectAsyncThrowException()
        {
            await this.SeedDatabase();

            var model = new EditProjectInputModel()
            {
                Name = "Edited Project Name",
                Description = "Edited Description",
                Styles = new List<int> { 2 },
            };

            var exception = await Assert.ThrowsAsync<NullReferenceException>(
                async () => await this.projectsService.UpdateAsync(3, model));

            Assert.Equal("Project is not found!", exception.Message);
        }

        [Fact] // public IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 6)
        public async Task GetAllProjectsReturnsCorrectCountPerPage()
        {
            await this.SeedDatabase();

            var result = this.projectsService.GetAll<SingleProjectViewModel>(1, 6);

            Assert.Single(result);
        }

        [Fact] // public T GetById<T>(int id)
        public async Task GetProjectById()
        {
            await this.SeedDatabase();

            var project = this.projectsService.GetById<SingleProjectViewModel>(1);

            Assert.Equal("LAKE HOUSE INTERIORS", project.Name);
            Assert.Equal(1, project.Id);
        }

        private async Task SeedDatabase()
        {
            var user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                TownId = 1,
                CompanyId = 2,
            };

            var project = new CreateProjectInputModel
            {
                Name = "LAKE HOUSE INTERIORS",
                Description = "КОПРОРАТИВЕН ОФИС FADATA СОФИЯ Офис пространство съобразено с нуждите и изискванията на AGILE WORKING SPACE.Оазис за сетивата и креативността.",
                IsRealized = "true",
                IsPublic = "true",
                CategoryId = 1,
            };
            project.Styles = new List<string> { "1" };

            var style1 = new Style()
            {
                Id = 1,
                Name = "Бохо",
            };

            await this.stylesRepository.AddAsync(style1);
            await this.stylesRepository.SaveChangesAsync();

            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg")
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/jpg",
                ContentDisposition = @"form-data; name='Data'; filename='dummy.jpg'",
            };

            project.Images = new List<IFormFile> { file };

            await this.projectsService.CreateAsync(project, user, "wwwroot");
        }

        public void Dispose()
        {
            this.dbContext.Database.EnsureDeleted();
            this.dbContext.Dispose();

            this.projectsRepository.Dispose();
            this.stylesRepository.Dispose();
        }
    }
}
