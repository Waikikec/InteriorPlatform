namespace InteriorPlatform.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using InteriorPlatform.Data.Models;
    using InteriorPlatform.Web.ViewModels.Designer;
    using InteriorPlatform.Web.ViewModels.Inquire;

    public interface IDesignersService
    {
        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetRandom<T>(int count);

        T GetById<T>(string id);

        Task CreateInquireAsync(InquireAssemblyViewModel model);

        Task SetAboutMeForDesigner(AboutMeInputModel model, string userId);

        ApplicationUser GetPhoto(string id);
    }
}
