namespace InteriorPlatform.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using InteriorPlatform.Data.Models;
    using InteriorPlatform.Web.ViewModels.Project;

    public interface IProjectsService
    {
        Task CreateAsync(CreateProjectInputModel input, ApplicationUser user, string imagePath);

        Task UpdateAsync(int id, EditProjectInputModel input);

        Task DeleteAsync(int id);

        IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 6);

        IEnumerable<T> GetRandom<T>(int count);

        IEnumerable<T> GetAllByUserId<T>(string id);

        T GetById<T>(int id);

        int GetCount();
        IEnumerable<T> GetBySearch<T>(object name, object category, object style);
    }
}
