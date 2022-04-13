namespace InteriorPlatform.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using InteriorPlatform.Data;
    using InteriorPlatform.Data.Models;
    using InteriorPlatform.Data.Repositories;
    using InteriorPlatform.Web.ViewModels.Project;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class ProjectsServiceTests : IDisposable
    {
        private readonly ApplicationDbContext dbContext;
        private readonly EfDeletableEntityRepository<Project> projectsRepository;
        private readonly EfDeletableEntityRepository<Style> stylesRepository;

        public ProjectsServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            this.dbContext = new ApplicationDbContext(options);

            this.projectsRepository = new EfDeletableEntityRepository<Project>(this.dbContext);
            this.stylesRepository = new EfDeletableEntityRepository<Style>(this.dbContext);

            //AutoMapperConfig.RegisterMappings(typeof(SingleDesignerViewModel).Assembly, typeof(ApplicationUser).Assembly);
        }

        [Fact] // public async Task CreateAsync(CreateProjectInputModel model, ApplicationUser user, string imagePath)
        public async Task AddingProjectSuccessfully()
        {
            var user = new ApplicationUser();
            user.TownId = 1;
            user.CompanyId = 2;
            user.Id = Guid.NewGuid().ToString();

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

            var service = new ProjectsService(this.projectsRepository, this.stylesRepository);
            await service.CreateAsync(project, user, "wwwroot");

            Assert.Equal(1, this.projectsRepository.All().Count());
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
