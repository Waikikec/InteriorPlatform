namespace InteriorPlatform.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;

    using InteriorPlatform.Data;
    using InteriorPlatform.Data.Models;
    using InteriorPlatform.Data.Repositories;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class CategoriesServiceTests
    {
        [Fact]
        public void GetCategoriesReturnsKeyValuePairsType()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var categoriesRepository = new EfDeletableEntityRepository<Category>(dbContext);

            var service = new CategoriesService(categoriesRepository);

            var result = service.GetCategories();

            Assert.IsAssignableFrom<IEnumerable<KeyValuePair<string, string>>>(result);
        }
    }
}
