namespace InteriorPlatform.Services.Data.Tests
{
    using System.Linq;

    using InteriorPlatform.Data;
    using InteriorPlatform.Data.Models;
    using InteriorPlatform.Data.Repositories;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class CategoriesServiceTests
    {
        [Fact] // IEnumerable<KeyValuePair<string, string>> GetCategories()
        public async void GetCategoriesCount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "InteriorPlatformDB").Options;

            using var dbContext = new ApplicationDbContext(options);
            dbContext.Categories.Add(new Category());
            dbContext.Categories.Add(new Category());
            dbContext.Categories.Add(new Category());
            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Category>(dbContext);
            var service = new CategoriesService(repository);
            Assert.Equal(3, service.GetCategories().Count());
        }
    }
}
