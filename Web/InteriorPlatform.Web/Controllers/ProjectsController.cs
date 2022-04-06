namespace InteriorPlatform.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using InteriorPlatform.Data.Models;
    using InteriorPlatform.Services.Data;
    using InteriorPlatform.Web.ViewModels.Designer;
    using InteriorPlatform.Web.ViewModels.Inquire;
    using InteriorPlatform.Web.ViewModels.Project;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using static InteriorPlatform.Common.GlobalConstants;

    public class ProjectsController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly IStylesService stylesService;
        private readonly IProjectsService projectsService;
        private readonly IDesignersService designersService;
        private readonly IWebHostEnvironment environment;
        private readonly UserManager<ApplicationUser> userManager;

        public ProjectsController(
            ICategoriesService categoriesService,
            IStylesService stylesService,
            IProjectsService projectsService,
            IDesignersService designersService,
            IWebHostEnvironment environment,
            UserManager<ApplicationUser> userManager)
        {
            this.categoriesService = categoriesService;
            this.stylesService = stylesService;
            this.projectsService = projectsService;
            this.designersService = designersService;
            this.environment = environment;
            this.userManager = userManager;
        }

        public IActionResult All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            const int ItemsPerPage = 6;
            var viewModel = new AllProjectsViewModel
            {
                Projects = this.projectsService.GetAll<SingleProjectViewModel>(id, ItemsPerPage),
                ProjectsCount = this.projectsService.GetCount(),
                PageNumber = id,
                ItemsPerPage = ItemsPerPage,
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> ById(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var viewModel = new InquireAssemblyViewModel
            {
                Project = this.projectsService.GetById<ByIdProjectViewModel>(id),
                Designer = this.designersService.GetById<SingleDesignerViewModel>(user.Id),
            };

            return this.View(viewModel);
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
                await this.projectsService.CreateAsync(model, user, $"{this.environment.WebRootPath}");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                model.CategoriesItems = this.categoriesService.GetCategories();
                model.StylesItems = this.stylesService.GetStyles();
                return this.View(model);
            }

            this.TempData["CreateProjectSuccess"] = ProjectCreatedSuccessfully;

            return this.RedirectToAction("All");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            await this.projectsService.DeleteAsync(id);
            return this.RedirectToAction(nameof(this.All));
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var inputModel = this.projectsService.GetById<EditProjectInputModel>(id);
            inputModel.CategoriesItems = this.categoriesService.GetCategories();
            inputModel.StylesItems = this.stylesService.GetStyles();
            return this.View(inputModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, EditProjectInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                inputModel.CategoriesItems = this.categoriesService.GetCategories();
                inputModel.StylesItems = this.stylesService.GetStyles();
                return this.View(inputModel);
            }

            await this.projectsService.UpdateAsync(id, inputModel);
            return this.RedirectToAction(nameof(this.ById), new { id });
        }
    }
}
