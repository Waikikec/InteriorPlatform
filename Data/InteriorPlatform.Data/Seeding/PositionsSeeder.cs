namespace InteriorPlatform.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using InteriorPlatform.Data.Models;

    public class PositionsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Positions.Any())
            {
                return;
            }

            var positions = new List<string>
            {
                "Архитект",
                "Ландшафт",
                "Интериорен дизайнер",
                "Продуктов дизайнер",
                "3Д Дизайнер",
                "Проектант",
                "Производител",
                "Студио",
                "Търговец",
                "Дистрибутор",
                "Инженер",
            };

            foreach (var position in positions)
            {
                await dbContext.AddAsync(new Position
                {
                    Name = position,
                });
            }
        }
    }
}
