namespace InteriorPlatform.Web.Areas.Administration.ViewComponents
{
    using System.Threading.Tasks;

    using InteriorPlatform.Data.Common.Repositories;
    using InteriorPlatform.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class PositionsViewComponent : ViewComponent
    {
        private readonly IDeletableEntityRepository<Position> dataRepository;

        public PositionsViewComponent(IDeletableEntityRepository<Position> dataRepository)
        {
            this.dataRepository = dataRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var positions = await this.dataRepository.AllWithDeleted().ToListAsync();
            return this.View(positions);
        }

    }
}
