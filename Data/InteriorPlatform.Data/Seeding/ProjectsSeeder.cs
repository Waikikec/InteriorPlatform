namespace InteriorPlatform.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using InteriorPlatform.Data.Models;

    internal class ProjectsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Projects.Any())
            {
                return;
            }

            var projects = new List<Project>()
            {
                new Project
                {
                    Name = "NFINITY HOUSE",
                    Description = "София, кв. Симеоново. Новопостроена къща за релакс и почивка е сгушена в полите на Витоша. Цялостното преразпределение на пространството позволява  разгръщането на просторна и светла дневна с многофункционална, компактна кухня, скрита зад бели релефни, високи до тавана врати, преминаващи в елегантна био  камина. Комфортна спа зона с джакузи, обшито с ипе, се свързва с дневния кът посредством стъклена плъзгаща врата в черна рамка. И двете пространства съвсем  естествено излизат в зеления и окъпан в слънчева светлина двор, където е разположен трапезарен кът и кът за почивка с комфортни шезлоги.",
                    IsRealized = true,
                    IsPublic = true,
                    CategoryId = 1,
                    TownId = 1,
                    CompanyId = 1,
                    AddedByUserId = "208982a5-4c65-46c4-aeec-661c3af647fa",
                },
                new Project
                {
                    Name = "NFINITY HOUSE",
                    Description = "София, кв. Симеоново. Новопостроена къща за релакс и почивка е сгушена в полите на Витоша. Цялостното преразпределение на пространството позволява  разгръщането на просторна и светла дневна с многофункционална, компактна кухня, скрита зад бели релефни, високи до тавана врати, преминаващи в елегантна био  камина. Комфортна спа зона с джакузи, обшито с ипе, се свързва с дневния кът посредством стъклена плъзгаща врата в черна рамка. И двете пространства съвсем  естествено излизат в зеления и окъпан в слънчева светлина двор, където е разположен трапезарен кът и кът за почивка с комфортни шезлоги.",
                    IsRealized = true,
                    IsPublic = true,
                    CategoryId = 1,
                    TownId = 1,
                    CompanyId = 1,
                    AddedByUserId = "208982a5-4c65-46c4-aeec-661c3af647fa",
                },
            };

            foreach (var project in projects)
            {
                await dbContext.Projects.AddAsync(project);
            }
        }
    }
}