namespace InteriorPlatform.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using InteriorPlatform.Data.Common.Repositories;
    using InteriorPlatform.Data.Models;
    using InteriorPlatform.Services.Mapping;

    public class DesignersService : IDesignersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;

        public DesignersService(IDeletableEntityRepository<ApplicationUser> usersRepository)
        {
            this.usersRepository = usersRepository;
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
    }
}
