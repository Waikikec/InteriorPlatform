namespace InteriorPlatform.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using InteriorPlatform.Data.Models;

    public class CitiesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Towns.Any())
            {
                return;
            }

            var towns = new List<string>
            {
                "София",
                "Стара Загора",
                "Варна",
                "Пловдив",
                "Плевен",
                "Велико Търново",
                "Бургас",
                "Благоевград",
                "Петрич",
                "Нова Загора",
            };

            foreach (var town in towns)
            {
                await dbContext.AddAsync(new Town
                {
                    Name = town,
                });
            }
        }
    }
}
