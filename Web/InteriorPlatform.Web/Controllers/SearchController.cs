namespace InteriorPlatform.Web.Controllers
{
    using InteriorPlatform.Services.Data;
    using InteriorPlatform.Web.ViewModels.Project;
    using InteriorPlatform.Web.ViewModels.Search;
    using Microsoft.AspNetCore.Mvc;

    public class SearchController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly IStylesService stylesService;
        private readonly IProjectsService projectsService;

        public SearchController(
            ICategoriesService categoriesService,
            IStylesService stylesService,
            IProjectsService projectsService)
        {
            this.categoriesService = categoriesService;
            this.stylesService = stylesService;
            this.projectsService = projectsService;
        }

        public IActionResult Index()
        {
            var model = new SearchIndexViewModel
            {
                CategoriesItems = this.categoriesService.GetCategories(),
                StylesItems = this.stylesService.GetStyles(),
            };
            return this.View(model);
        }

        [HttpPost]
        public IActionResult Index(SearchIndexInputModel input)
        {
            var inputModel = new SearchListModel
            {
                Projects = this.projectsService.GetBySearch<SingleProjectViewModel>(input.Name, input.CategoryId, input.Styles),
            };
            return this.View(inputModel);
        }
    }
}
