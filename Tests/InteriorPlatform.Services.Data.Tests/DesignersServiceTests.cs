namespace InteriorPlatform.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using InteriorPlatform.Data;
    using InteriorPlatform.Data.Models;
    using InteriorPlatform.Data.Repositories;
    using InteriorPlatform.Web.ViewModels.Designer;
    using InteriorPlatform.Web.ViewModels.Inquire;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class DesignersServiceTests
    {
        private readonly IDesignersService designersService;

        public DesignersServiceTests()
        {

        }

        [Fact] // public async Task CreateInquireAsync(InquireAssemblyViewModel model, ApplicationUser user)
        public async Task AddingInquireSuccessfully()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var usersRepository = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var inquiresRepository = new EfDeletableEntityRepository<Inquire>(dbContext);

            var designersService = new DesignersService(usersRepository, inquiresRepository);
            var user = new ApplicationUser();

            var inquireAssembly = new InquireAssemblyViewModel
            {
                Inquire = new InquireInputModel
                {
                    Name = "Мартин Янков",
                    PhoneNumber = "0886123123",
                    Email = "martin_iankov_test@abv.bg",
                    Info = "Търся помощ за обзавеждане на апартамент",
                },
            };

            await designersService.CreateInquireAsync(inquireAssembly, user);

            Assert.Equal(1, inquiresRepository.All().Count());
        }

        [Fact] // public IEnumerable<T> GetAll<T>()
        public async Task GetAllDesignersAsyncWorksCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var usersRepository = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var inquiresRepository = new EfDeletableEntityRepository<Inquire>(dbContext);

            var designersService = new DesignersService(usersRepository, inquiresRepository);

            var firstDesigner = new ApplicationUser()
            {
                Email = "martin_test1@abv.bg",
                FirstName = "Мартин",
                LastName = "Янков",
                UserName = "Мартин Янков",
                PasswordHash = "marto123",
                Company = new Company { Name = "Нова компания" },
            };

            await usersRepository.AddAsync(firstDesigner);
            await usersRepository.SaveChangesAsync();

            var result = designersService.GetAll<SingleDesignerViewModel>();
            var count = result.Count();

            Assert.Equal(1, count);
        }
    }
}
