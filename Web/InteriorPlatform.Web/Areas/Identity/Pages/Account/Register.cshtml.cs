namespace InteriorPlatform.Web.Areas.Identity.Pages.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    using InteriorPlatform.Data.Models;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.WebUtilities;
    using Microsoft.Extensions.Logging;

    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<RegisterModel> logger;
        private readonly IEmailSender emailSender;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Полето '{0}' е задължително.")]
            [EmailAddress]
            [Display(Name = "Имейл")]
            [StringLength(100, ErrorMessage = "Полето '{0}' трябва да бъде валиден.", MinimumLength = 6)]
            public string Email { get; set; }

            [Required(ErrorMessage = "Полето '{0}' е задължително.")]
            [StringLength(100, ErrorMessage = "Полето '{0}' трябва да бъде между {2} и максимум {1} символа.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Парола")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Потвърди паролата")]
            [Compare("Password", ErrorMessage = "Паролата не съвпада")]
            public string ConfirmPassword { get; set; }

            [Required(ErrorMessage = "Полето '{0}' е задължително.")]
            [Display(Name = "Име")]
            [StringLength(30, ErrorMessage = "Полето '{0}' трябва да бъде между {2} и максимум {1} символа.", MinimumLength = 3)]
            public string FirstName { get; set; }

            [Required(ErrorMessage = "Полето '{0}' е задължително.")]
            [Display(Name = "Фамилия")]
            [StringLength(30, ErrorMessage = "Полето '{0}' трябва да бъде между {2} и максимум {1} символа.", MinimumLength = 3)]
            public string LastName { get; set; }

            [Required(ErrorMessage = "Полето '{0}' е задължително.")]
            [Display(Name = "Фирма")]
            [StringLength(30, ErrorMessage = "Полето '{0}' трябва да бъде между {2} и максимум {1} символа.", MinimumLength = 3)]
            public string Company { get; set; }

            [Required(ErrorMessage = "Полето '{0}' е задължително.")]
            [Display(Name = "Длъжност")]
            [Range(1, 10, ErrorMessage = "Избери длъжност")]
            public int PositionId { get; set; }

            [Required(ErrorMessage = "Полето '{0}' е задължително.")]
            [Display(Name = "Град")]
            [Range(1, 10, ErrorMessage = "Избери град")]
            public int TownId { get; set; }

            [Required(ErrorMessage = "Полето '{0}' е задължително.")]
            [Display(Name = "Телефон")]
            [StringLength(30, ErrorMessage = "Полето '{0}' трябва да бъде между {2} и максимум {1} символа.", MinimumLength = 3)]
            public string PhoneNumber { get; set; }

            [Required(ErrorMessage = "Полето '{0}' е задължително.")]
            public bool Checkbox { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            this.ReturnUrl = returnUrl;
            this.ExternalLogins = (await this.signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= this.Url.Content("~/");
            this.ExternalLogins = (await this.signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (this.ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    Email = this.Input.Email.Trim(),
                    FirstName = this.Input.FirstName.Trim(),
                    LastName = this.Input.LastName.Trim(),
                    UserName = this.Input.FirstName.Trim() + " " + this.Input.LastName.Trim(),
                    PasswordHash = this.Input.Password,
                    Company = new Company { Name = this.Input.Company },
                    TownId = this.Input.TownId,
                    PositionId = this.Input.PositionId,
                    PhoneNumber = this.Input.PhoneNumber.Trim(),
                };

                var result = await this.userManager.CreateAsync(user, this.Input.Password);
                if (result.Succeeded)
                {
                    this.logger.LogInformation("User created a new account with password.");

                    var code = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = this.Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: this.Request.Scheme);

                    await this.emailSender.SendEmailAsync(this.Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (this.userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return this.RedirectToPage("RegisterConfirmation", new { email = this.Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await this.signInManager.SignInAsync(user, isPersistent: false);
                        return this.LocalRedirect(returnUrl);
                    }
                }

                foreach (var error in result.Errors)
                {
                    this.ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return this.Page();
        }
    }
}
