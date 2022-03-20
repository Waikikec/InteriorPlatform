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
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class DesignersController : Controller
    {
        private readonly IDesignersService designersService;
        private readonly IProjectsService projectsService;
        private readonly IInquiresService inquiresService;
        private readonly UserManager<ApplicationUser> userManager;

        public DesignersController(
            IDesignersService designersService,
            IProjectsService projectsService,
            IInquiresService inquiresService,
            UserManager<ApplicationUser> userManager)
        {
            this.designersService = designersService;
            this.projectsService = projectsService;
            this.inquiresService = inquiresService;
            this.userManager = userManager;
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
                Designer = this.designersService.GetById<SingleDesignerViewModel>(id),
                Projects = this.projectsService.GetAllByUserId<SingleProjectViewModel>(id),
                Inquires = this.inquiresService.GetAllInquiresByUserId<InquireViewModel>(id),
            };

            return this.View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> Inquire(InquireAssemblyViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("All", "Designers");
            }

            var user = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.designersService.CreateInquireAsync(model, user);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(model);
            }

            this.TempData["Message"] = "Inquire has been sent successfully!";

            return this.RedirectToAction("All", "Designers");
        }
    }
}
