namespace InteriorPlatform.Web.Controllers
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using InteriorPlatform.Data.Models;
    using InteriorPlatform.Services;
    using InteriorPlatform.Services.Data;
    using InteriorPlatform.Web.ViewModels.Designer;
    using InteriorPlatform.Web.ViewModels.Image;
    using InteriorPlatform.Web.ViewModels.Inquire;
    using InteriorPlatform.Web.ViewModels.Project;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using static InteriorPlatform.Common.GlobalConstants;

    public class DesignersController : Controller
    {
        private readonly IDesignersService designersService;
        private readonly IProjectsService projectsService;
        private readonly IInquiresService inquiresService;
        private readonly ICloudImageService imageService;
        private readonly IImageDbService imageDbService;
        private readonly IWebHostEnvironment environment;
        private readonly UserManager<ApplicationUser> userManager;

        public DesignersController(
            IDesignersService designersService,
            IProjectsService projectsService,
            IInquiresService inquiresService,
            ICloudImageService imageService,
            IImageDbService imageDbService,
            IWebHostEnvironment environment,
            UserManager<ApplicationUser> userManager)
        {
            this.designersService = designersService;
            this.projectsService = projectsService;
            this.inquiresService = inquiresService;
            this.imageService = imageService;
            this.imageDbService = imageDbService;
            this.environment = environment;
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

            this.TempData["InquireCreatedSuccessfully"] = InquireCreatedSuccessfully;

            return this.RedirectToAction("All", "Designers");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Upload(CloudinaryImageInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var userId = this.userManager.GetUserId(this.User);
            var user = await this.userManager.FindByIdAsync(userId);

            var imageResult = await this.imageService.UploadImageAsync(model.Image);

            var imageUrl = imageResult.SecureUri.AbsoluteUri;
            var imagePublicId = imageResult.PublicId;

            var newImage = new CloudImage
            {
                PicturePublicId = imagePublicId,
                PictureUrl = imageUrl,
            };

            user.ProfilePicture = newImage;

            await this.imageDbService.WriteToDatabaseAsync(newImage);

            return this.RedirectToAction("All", "Designers");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SetAboutMe(AboutMeInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("All", "Designers");
            }

            var userId = this.userManager.GetUserId(this.User);

            try
            {
                await this.designersService.SetAboutMeForDesigner(model, userId);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }

            return this.RedirectToAction("All", "Designers");
        }

        public string GetProfilePicture(string id)
        {
            var user = this.designersService.GetPhoto(id);

            if (user.ProfilePicture == null)
            {
                var placeholder = "placeholder.jpg";
                string localImage = Path.Combine(this.environment.WebRootPath, "assets/img", placeholder);

                return localImage;
            }

            var cloudImage = user.ProfilePicture.PictureUrl;
            return cloudImage;
        }
    }
}
