namespace InteriorPlatform.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using InteriorPlatform.Data.Models;
    using InteriorPlatform.Web.ViewModels.Project;

    public interface IProjectsService
    {
        Task CreateAsync(CreateProjectInputModel input, ApplicationUser user, string imagePath);

        IEnumerable<T> GetAll<T>();
    }
}
