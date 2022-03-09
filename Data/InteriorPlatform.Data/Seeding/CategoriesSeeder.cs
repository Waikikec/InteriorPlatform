namespace InteriorPlatform.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using InteriorPlatform.Data.Models;

    public class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            var categories = new List<string>
            {
                "Апартамент",
                "Къща",
                "Офис",
                "Всекидневна",
                "Кухня",
                "Трапезария",
                "Спалня",
                "Гардеробна",
                "Баня",
                "Детска стая",
                "Зона за отдих",
                "Заведение",
                "Бар",
                "Кафе",
                "Магазин",
                "Шоурум",
                "Изложбена площ",
                "Фитнес / спорт",
                "Общи части",
                "Салон за красота",
                "Фризьорски салон",
                "Бръснарница",
                "Бръснарница",
                "Зъболекарски / Дентален кабинет",
                "Жилищни сгради",
                "Обществени сгради",
                "Административни сгради",
                "Производствени сгради",
                "Транспортни сгради",
                "Сгради културно-историческо наследство",
                "Мебели",
                "Осветление",
                "Техника",
                "Бижута",
            };

            foreach (var category in categories)
            {
                await dbContext.AddAsync(new Category
                {
                    Name = category,
                });
            }
        }
    }
}
