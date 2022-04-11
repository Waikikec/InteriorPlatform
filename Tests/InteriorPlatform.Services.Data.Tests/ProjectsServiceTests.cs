namespace InteriorPlatform.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
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

    public class ProjectsServiceTests
    {
        [Fact] // public async Task CreateAsync(CreateProjectInputModel model, ApplicationUser user, string imagePath)
        public async Task CreateAsyncShouldAddProjectSuccessfully()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var projectsRepo = new EfDeletableEntityRepository<Project>(dbContext);
            var stylesRepo = new EfDeletableEntityRepository<Style>(dbContext);

            var service = new ProjectsService(projectsRepo, stylesRepo);
            var user = new ApplicationUser();

            var project = new CreateProjectInputModel
            {
                Name = "LAKE HOUSE INTERIORS",
                Description = "КОПРОРАТИВЕН ОФИС FADATA СОФИЯ Офис пространство съобразено с нуждите и изискванията на AGILE WORKING SPACE.Оазис за сетивата и креативността.",
                IsRealized = "true",
                IsPublic = "true",
                CategoryId = 1,
            };

            project.Styles = new List<string> { "1", "2", };

            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.jpg")
            {
                Headers = new HeaderDictionary(),
                ContentType = "multipart/form-data",
            };

            project.Images = new List<IFormFile> { file };

            await service.CreateAsync(project, user, "wwwroot");

            Assert.Equal(1, service.GetCount());
        }
    }
}
