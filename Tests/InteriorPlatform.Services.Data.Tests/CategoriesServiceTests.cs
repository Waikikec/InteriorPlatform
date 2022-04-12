namespace InteriorPlatform.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using InteriorPlatform.Data;
    using InteriorPlatform.Data.Common.Repositories;
    using InteriorPlatform.Data.Models;
    using InteriorPlatform.Data.Repositories;
    using Microsoft.EntityFrameworkCore;
    using Moq;
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

            var list = new List<Category>();
            var mockRepo = new Mock<IRepository<Category>>();

            //mockRepo
            //    .Setup(x => x.All())
            //    .Returns(list.AsQueryable());

            //mockRepo
            //    .Setup(x => x.AddAsync(It.IsAny<Category>()))
            //    .Callback((Category category) => list.Add(category));

            var service = new CategoriesService(categoriesRepository);

            var result = service.GetCategories();

            Assert.IsAssignableFrom<IEnumerable<KeyValuePair<string, string>>>(result);
        }
    }
}
