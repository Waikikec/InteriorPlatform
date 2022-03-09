namespace InteriorPlatform.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using InteriorPlatform.Data.Models;

    public class StylesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Styles.Any())
            {
                return;
            }

            var styles = new List<string>
            {
                "Модерен",
                "Еклектичен",
                "Индустриален",
                "Винтидж",
                "Бохо",
                "Луксозен",
                "Ретро",
                "Минималистичен",
                "Съвременен",
                "Градски",
                "Традиционен",
                "Провинциален",
            };

            foreach (var style in styles)
            {
                await dbContext.AddAsync(new Style
                {
                    Name = style,
                });
            }
        }
    }
}
