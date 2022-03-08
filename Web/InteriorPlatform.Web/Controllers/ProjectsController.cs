namespace InteriorPlatform.Web.Controllers
{
    using System.Threading.Tasks;

    using InteriorPlatform.Web.ViewModels.Project;
    using Microsoft.AspNetCore.Mvc;

    public class ProjectsController : Controller
    {
        public ProjectsController()
        {

        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProjectInputModel input)
        {
            return this.View(input);
        }
    }
}
