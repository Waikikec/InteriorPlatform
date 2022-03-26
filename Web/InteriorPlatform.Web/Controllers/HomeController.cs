namespace InteriorPlatform.Web.Controllers
{
    using System.Diagnostics;

    using InteriorPlatform.Services.Data;
    using InteriorPlatform.Web.ViewModels;
    using InteriorPlatform.Web.ViewModels.Designer;
    using InteriorPlatform.Web.ViewModels.Home;
    using InteriorPlatform.Web.ViewModels.Project;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IProjectsService projectsService;
        private readonly IDesignersService designersService;

        public HomeController(
            IProjectsService projectsService,
            IDesignersService designersService)
        {
            this.projectsService = projectsService;
            this.designersService = designersService;
        }

        public IActionResult Index()
        {
            var viewModel = new HomePageViewModel
            {
                RandomProjects = this.projectsService.GetRandom<SingleProjectViewModel>(8),
                RandomDesigners = this.designersService.GetRandom<SingleDesignerViewModel>(4),
            };
            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
