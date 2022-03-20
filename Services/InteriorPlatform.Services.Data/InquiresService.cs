namespace InteriorPlatform.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using InteriorPlatform.Data.Common.Repositories;
    using InteriorPlatform.Data.Models;
    using InteriorPlatform.Services.Mapping;

    public class InquiresService : IInquiresService
    {
        private readonly IDeletableEntityRepository<Inquire> inquiresRepository;

        public InquiresService(
            IDeletableEntityRepository<Inquire> inquiresRepository)
        {
            this.inquiresRepository = inquiresRepository;
        }

        public IEnumerable<T> GetAllInquiresByUserId<T>(string id)
        {
            var inquires = this.inquiresRepository
                .AllAsNoTracking()
                .Where(x => x.AddedByUserId == id)
                .To<T>()
                .ToList();

            return inquires;
        }
    }
}
