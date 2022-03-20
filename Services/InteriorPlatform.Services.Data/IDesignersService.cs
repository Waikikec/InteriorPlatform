namespace InteriorPlatform.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using InteriorPlatform.Data.Models;
    using InteriorPlatform.Web.ViewModels.Inquire;

    public interface IDesignersService
    {
        IEnumerable<T> GetAll<T>();

        T GetById<T>(string id);

        Task CreateInquireAsync(InquireAssemblyViewModel model, ApplicationUser user);
    }
}
