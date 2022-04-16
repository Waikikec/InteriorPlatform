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
            var assemblyModel = new SearchAssemblyModel
            {
                SearchViewModel = new SearchIndexViewModel
                {
                    CategoriesItems = this.categoriesService.GetCategories(),
                    StylesItems = this.stylesService.GetStyles(),
                },
            };

            return this.View(assemblyModel);
        }

        [HttpPost]
        public IActionResult Index(SearchAssemblyModel input)
        {
            var assemblyModel = new SearchAssemblyModel
            {
                SearchListModel = new SearchListModel
                {
                    Projects = this.projectsService
                    .GetBySearch<SingleProjectViewModel>(
                        input.SearchInputModel.Name,
                        input.SearchInputModel.CategoryId,
                        input.SearchInputModel.Styles),
                },
                SearchViewModel = new SearchIndexViewModel
                {
                    CategoriesItems = this.categoriesService.GetCategories(),
                    StylesItems = this.stylesService.GetStyles(),
                },
            };

            return this.View(assemblyModel);
        }
    }
}