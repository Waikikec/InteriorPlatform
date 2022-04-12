namespace InteriorPlatform.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using InteriorPlatform.Data;
    using InteriorPlatform.Data.Models;
    using InteriorPlatform.Data.Repositories;
    using InteriorPlatform.Services.Mapping;
    using InteriorPlatform.Web.ViewModels.Designer;
    using InteriorPlatform.Web.ViewModels.Inquire;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class DesignersServiceTests : IDisposable
    {
        private readonly EfDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly EfDeletableEntityRepository<Inquire> inquiresRepository;
        private readonly ApplicationDbContext dbContext;

        public DesignersServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "db").Options;

            this.dbContext = new ApplicationDbContext(options);

            this.usersRepository = new EfDeletableEntityRepository<ApplicationUser>(this.dbContext);
            this.inquiresRepository = new EfDeletableEntityRepository<Inquire>(this.dbContext);

            AutoMapperConfig.RegisterMappings(typeof(SingleDesignerViewModel).Assembly, typeof(ApplicationUser).Assembly);
        }

        [Fact] // public async Task CreateInquireAsync(InquireAssemblyViewModel model, ApplicationUser user)
        public async Task AddingInquireSuccessfully()
        {
            var designersService = new DesignersService(this.usersRepository, this.inquiresRepository);

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

            Assert.Equal(1, this.inquiresRepository.All().Count());
        }

        [Fact] // public IEnumerable<T> GetAll<T>()
        public async Task GetAllDesignersAsyncWorksCorrectly()
        {
            var firstDesigner = new ApplicationUser()
            {
                Id = "UserTest1",
                Email = "martin_test1@abv.bg",
                FirstName = "Мартин",
                LastName = "Янков",
                UserName = "Мартин Янков",
                PasswordHash = "marto123",
                Company = new Company { Id = 1, Name = "Нова компания" },
            };

            var secondDesigner = new ApplicationUser()
            {
                Id = "UserTest2",
                Email = "teodor_test1@abv.bg",
                FirstName = "Теодор",
                LastName = "Янков",
                UserName = "Теодор Янков",
                PasswordHash = "tedo123",
                Company = new Company { Id = 2, Name = "Нова компания 2" },
            };

            this.dbContext.Users.Add(firstDesigner);
            this.dbContext.Users.Add(secondDesigner);
            await this.dbContext.SaveChangesAsync();

            //var designersService = new DesignersService(this.usersRepository, this.inquiresRepository);
            //var result = designersService.GetAll<SingleDesignerViewModel>().ToList();

            Assert.Equal(2, this.usersRepository.All().Count());
        }

        public void Dispose() => this.dbContext.Database.EnsureDeleted();
    }
}
