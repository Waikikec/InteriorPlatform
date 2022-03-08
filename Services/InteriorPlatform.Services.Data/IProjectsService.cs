namespace InteriorPlatform.Services.Data
{
    using System.Threading.Tasks;

    using InteriorPlatform.Web.ViewModels.Project;

    public interface IProjectsService
    {
        Task CreateAsync(CreateProjectInputModel input, string userId, string imagePath);
    }
}
