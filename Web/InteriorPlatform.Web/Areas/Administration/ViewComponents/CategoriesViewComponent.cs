namespace InteriorPlatform.Web.Areas.Administration.ViewComponents
{
    using System.Threading.Tasks;

    using InteriorPlatform.Data.Common.Repositories;
    using InteriorPlatform.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class CategoriesViewComponent : ViewComponent
    {
        private readonly IDeletableEntityRepository<Category> dataRepository;

        public CategoriesViewComponent(IDeletableEntityRepository<Category> dataRepository)
        {
            this.dataRepository = dataRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await this.dataRepository.AllWithDeleted().ToListAsync();
            return this.View(categories);
        }

    }
}
