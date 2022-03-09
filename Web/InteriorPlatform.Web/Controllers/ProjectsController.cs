namespace InteriorPlatform.Web.Controllers
{
    using InteriorPlatform.Services.Data;
    using InteriorPlatform.Web.ViewModels.Project;
    using Microsoft.AspNetCore.Mvc;

    public class ProjectsController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly IStylesService stylesService;

        public ProjectsController(
            ICategoriesService categoriesService,
            IStylesService stylesService)
        {
            this.categoriesService = categoriesService;
            this.stylesService = stylesService;
        }

        public IActionResult Create()
        {
            var viewModel = new CreateProjectInputModel
            {
                CategoriesItems = this.categoriesService.GetCategories(),
                StylesItems = this.stylesService.GetStyles(),
            };
            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(CreateProjectInputModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                viewModel.CategoriesItems = this.categoriesService.GetCategories();
                viewModel.StylesItems = this.stylesService.GetStyles();
                return this.View(viewModel);
            }

            return this.Redirect("/");
        }
    }
}
