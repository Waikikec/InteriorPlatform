namespace InteriorPlatform.Web.Controllers
{
    using InteriorPlatform.Services.Data;
    using InteriorPlatform.Web.ViewModels.Designer;
    using InteriorPlatform.Web.ViewModels.Project;
    using Microsoft.AspNetCore.Mvc;

    public class DesignersController : Controller
    {
        private readonly IDesignersService designersService;
        private readonly IProjectsService projectsService;

        public DesignersController(
            IDesignersService designersService,
            IProjectsService projectsService)
        {
            this.designersService = designersService;
            this.projectsService = projectsService;
        }

        public IActionResult All()
        {
            var viewModel = new AllDesignersViewModel
            {
                Designers = this.designersService.GetAll<SingleDesignerViewModel>(),
            };

            return this.View(viewModel);
        }

        public IActionResult ById(string id)
        {
            var viewModel = new ByIdDesignerViewModel
            {
                Projects = this.projectsService.GetAllByUserId<SingleProjectViewModel>(id),
            };

            return this.View(viewModel);
        }
    }
}
