namespace InteriorPlatform.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using InteriorPlatform.Data.Models;

    public class CompaniesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Companies.Any())
            {
                return;
            }

            var companies = new List<string>
            {
                "2Studio",
                "FURAI DESIGN STUDIO",
                "Студио Амфион",
                "STUDIO L DESIGN",
                "SOFT CASE Studio",
                "LZ architecture",
                "Studio Straff",
                "COZY. design studio",
                "Apartment101",
                "MI CONCRETE",
                "SYNERGYDESIGN",
                "Ателие М+",
                "DOME Studio",
                "HIGH - END DESIGN STUDIO",
                "CASA ART Interior",
                "Workbox Studio",
                "NANKOV DESIGN",
                "MOLT Design Group",
                "Sepya Design Place",
                "InterYour",
                "Template Ltd.",
                "RS design",
                "IXDesign",
            };

            foreach (var company in companies)
            {
                await dbContext.AddAsync(new Company
                {
                    Name = company,
                });
            }
        }
    }
}
