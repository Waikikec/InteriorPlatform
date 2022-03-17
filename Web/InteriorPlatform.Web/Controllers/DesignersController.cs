namespace InteriorPlatform.Web.Controllers
{
    using InteriorPlatform.Services.Data;
    using InteriorPlatform.Web.ViewModels.Designer;
    using Microsoft.AspNetCore.Mvc;

    public class DesignersController : Controller
    {
        private readonly IDesignersService designersService;

        public DesignersController(IDesignersService designersService)
        {
            this.designersService = designersService;
        }

        public IActionResult All()
        {
            var viewModel = new AllDesignersViewModel
            {
                Designers = this.designersService.GetAll<SingleDesignerViewModel>(),
            };

            return this.View(viewModel);
        }
    }
}
