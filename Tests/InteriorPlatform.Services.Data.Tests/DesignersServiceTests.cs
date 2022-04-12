namespace InteriorPlatform.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
        private readonly ApplicationDbContext dbContext;
        private readonly EfDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly EfDeletableEntityRepository<Inquire> inquiresRepository;

        public DesignersServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

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
        public async Task GetAllDesignersReturnCorrectTypeAndCount()
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

            await this.dbContext.Users.AddAsync(firstDesigner);
            await this.dbContext.Users.AddAsync(secondDesigner);
            await this.dbContext.SaveChangesAsync();

            var designersService = new DesignersService(this.usersRepository, this.inquiresRepository);
            var result = designersService.GetAll<SingleDesignerViewModel>();

            Assert.IsType<List<SingleDesignerViewModel>>(result);
            Assert.Equal(2, this.usersRepository.All().Count());
        }

        [Fact] // public T GetById<T>(string id)
        public async Task GetByIdDesignerCorrectly()
        {
            var firstDesigner = new ApplicationUser()
            {
                Id = "UserTest3",
                Email = "martin_test1@abv.bg",
                FirstName = "Мартин",
                LastName = "Янков",
                UserName = "Мартин Янков",
                PasswordHash = "marto123",
                Company = new Company { Id = 1, Name = "Нова компания" },
            };

            await this.dbContext.Users.AddAsync(firstDesigner);
            await this.dbContext.SaveChangesAsync();

            var savedDesigner = this.usersRepository.All().FirstOrDefault(x => x.Id == "UserTest3");

            Assert.Equal("UserTest3", savedDesigner.Id);
        }

        [Fact] // public IEnumerable<T> GetRandom<T>(int count)
        public async Task GetRandomDesignersByGivenNumber()
        {
            var firstDesigner = new ApplicationUser()
            {
                Id = "UserTest3",
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

            await this.dbContext.Users.AddAsync(firstDesigner);
            await this.dbContext.Users.AddAsync(secondDesigner);
            await this.dbContext.SaveChangesAsync();

            var randomDesigners = this.usersRepository.All();

            Assert.Equal(2, randomDesigners.Count());
        }

        public void Dispose()
        {
            this.dbContext.Database.EnsureDeleted();
            this.dbContext.Dispose();

            this.usersRepository.Dispose();
            this.inquiresRepository.Dispose();
        }
    }
}
