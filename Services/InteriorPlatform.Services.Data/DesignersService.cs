namespace InteriorPlatform.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using InteriorPlatform.Data.Common.Repositories;
    using InteriorPlatform.Data.Models;
    using InteriorPlatform.Services.Mapping;
    using InteriorPlatform.Web.ViewModels.Designer;
    using InteriorPlatform.Web.ViewModels.Inquire;

    public class DesignersService : IDesignersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IDeletableEntityRepository<Inquire> inquiresRepository;

        public DesignersService(
            IDeletableEntityRepository<ApplicationUser> usersRepository,
            IDeletableEntityRepository<Inquire> inquiresRepository)
        {
            this.usersRepository = usersRepository;
            this.inquiresRepository = inquiresRepository;
        }

        public async Task SetAboutMeForDesigner(AboutMeInputModel model, string userId)
        {
            var currentUser = this.usersRepository
                .All()
                .FirstOrDefault(x => x.Id == userId);

            if (currentUser == null)
            {
                throw new NullReferenceException("User is not found!");
            }

            currentUser.AboutMe = model.AboutMe;

            await this.usersRepository.SaveChangesAsync();
        }

        public async Task CreateInquireAsync(InquireAssemblyViewModel model)
        {
            var inquire = new Inquire
            {
                Name = model.Inquire.Name,
                PhoneNumber = model.Inquire.PhoneNumber,
                Email = model.Inquire.Email,
                Info = model.Inquire.Info,
                AddedByUserId = model.Inquire.AddedByUserId,
            };

            await this.inquiresRepository.AddAsync(inquire);
            await this.inquiresRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>()
        {
            var users = this.usersRepository
                .AllAsNoTracking()
                .To<T>()
                .ToList();

            return users;
        }

        public T GetById<T>(string id)
        {
            var user = this.usersRepository
                .AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();

            return user;
        }

        public ApplicationUser GetPhoto(string id)
        {
            var user = this.usersRepository
                .Get(null, null, "ProfilePicture")
                .FirstOrDefault(x => x.Id == id);

            return user;
        }

        public IEnumerable<T> GetRandom<T>(int count)
        {
            return this.usersRepository
                .AllAsNoTracking()
                .OrderBy(x => Guid.NewGuid())
                .Take(count)
                .To<T>()
                .ToList();
        }
    }
}
