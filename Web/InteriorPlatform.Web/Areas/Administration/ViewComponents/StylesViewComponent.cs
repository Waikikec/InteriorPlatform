namespace InteriorPlatform.Web.Areas.Administration.ViewComponents
{
    using System.Threading.Tasks;

    using InteriorPlatform.Data.Common.Repositories;
    using InteriorPlatform.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class StylesViewComponent : ViewComponent
    {
        private readonly IDeletableEntityRepository<Style> dataRepository;

        public StylesViewComponent(IDeletableEntityRepository<Style> dataRepository)
        {
            this.dataRepository = dataRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var styles = await this.dataRepository.AllWithDeleted().ToListAsync();
            return this.View(styles);
        }
    }
}
