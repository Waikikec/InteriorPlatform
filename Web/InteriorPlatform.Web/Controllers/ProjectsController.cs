namespace InteriorPlatform.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using InteriorPlatform.Data.Models;
    using InteriorPlatform.Services.Data;
    using InteriorPlatform.Web.ViewModels.Project;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ProjectsController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly IStylesService stylesService;
        private readonly IProjectsService projectsService;
        private readonly IWebHostEnvironment environment;
        private readonly UserManager<ApplicationUser> userManager;

        public ProjectsController(
            ICategoriesService categoriesService,
            IStylesService stylesService,
            IProjectsService projectsService,
            IWebHostEnvironment environment,
            UserManager<ApplicationUser> userManager)
        {
            this.categoriesService = categoriesService;
            this.stylesService = stylesService;
            this.projectsService = projectsService;
            this.environment = environment;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Create()
        {
            var model = new CreateProjectInputModel
            {
                CategoriesItems = this.categoriesService.GetCategories(),
                StylesItems = this.stylesService.GetStyles(),
            };
            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateProjectInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.CategoriesItems = this.categoriesService.GetCategories();
                model.StylesItems = this.stylesService.GetStyles();
                return this.View(model);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.projectsService.CreateAsync(model, user.Id, $"{this.environment.WebRootPath}");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                model.CategoriesItems = this.categoriesService.GetCategories();
                model.StylesItems = this.stylesService.GetStyles();
                return this.View(model);
            }

            this.TempData["Message"] = "Project has been created successfully!";

            return this.Redirect("/");
        }
    }
}
