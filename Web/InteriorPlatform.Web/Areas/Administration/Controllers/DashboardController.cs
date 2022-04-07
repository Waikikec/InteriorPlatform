namespace InteriorPlatform.Web.Areas.Administration.Controllers
{
    using InteriorPlatform.Services.Data;
    using InteriorPlatform.Web.ViewModels.Administration.Dashboard;

    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : AdministrationController
    {
        private readonly ISettingsService settingsService;

        public DashboardController(ISettingsService settingsService)
        {
            this.settingsService = settingsService;
        }

        public IActionResult Index(IndexTabViewModel viewModel)
        {
            if (viewModel == null)
            {
                viewModel = new IndexTabViewModel
                {
                    ActiveTab = Tab.Categories,
                };
            }

            return this.View(viewModel);
        }

        public IActionResult SwitchToTabs(string tabName)
        {
            var viewModel = new IndexTabViewModel();

            viewModel.ActiveTab = tabName switch
            {
                "Positions" => Tab.Positions,
                "Categories" => Tab.Categories,
                "Styles" => Tab.Styles,
                _ => Tab.Positions,
            };

            return this.RedirectToAction(nameof(DashboardController.Index), viewModel);
        }
    }
}
