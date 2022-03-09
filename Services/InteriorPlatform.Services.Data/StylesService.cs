namespace InteriorPlatform.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using InteriorPlatform.Data.Common.Repositories;
    using InteriorPlatform.Data.Models;

    public class StylesService : IStylesService
    {
        private readonly IDeletableEntityRepository<Style> stylesRepository;

        public StylesService(IDeletableEntityRepository<Style> stylesRepository)
        {
            this.stylesRepository = stylesRepository;
        }

        public IEnumerable<KeyValuePair<string, string>> GetStyles()
        {
            return this.stylesRepository
                .AllAsNoTracking()
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                })
                .ToList()
                .Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name))
                .OrderBy(x => x.Value);
        }
    }
}
