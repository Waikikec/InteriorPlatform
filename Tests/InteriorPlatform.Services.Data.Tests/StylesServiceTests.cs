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

    public class StylesServiceTests
    {
        [Fact]
        public void GetStylesReturnsKeyValuePairsType()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var stylesRepository = new EfDeletableEntityRepository<Style>(dbContext);

            var service = new StylesService(stylesRepository);

            var result = service.GetStyles();

            Assert.IsAssignableFrom<IEnumerable<KeyValuePair<string, string>>>(result);
        }
    }
}
