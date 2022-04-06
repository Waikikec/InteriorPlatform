namespace InteriorPlatform.Web.Controllers
{
    using System.Threading.Tasks;

    using InteriorPlatform.Services.Messaging;
    using InteriorPlatform.Web.ViewModels.Contacts;
    using Microsoft.AspNetCore.Mvc;

    using static InteriorPlatform.Common.GlobalConstants;

    public class ContactsController : BaseController
    {
        private readonly IEmailSender emailSender;

        public ContactsController(IEmailSender emailSender)
        {
            this.emailSender = emailSender;
        }

        public IActionResult Index()
        {
            this.ViewData["MyTomTomKey"] = MyTomTomKey;
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(ContactUsInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var name = model.FirstName + " " + model.LastName;
            var email = model.Email;
            var subject = model.Subject;
            var content = model.Message;

            await this.emailSender.SendPlainEmailAsync(email, name, FabDesignSupportEmail, subject, content, null);

            this.TempData["MessageSentSuccessfully"] = MessageSentSuccessfully;

            return this.RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
