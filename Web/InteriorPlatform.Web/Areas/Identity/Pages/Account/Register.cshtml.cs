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
            [Required]
            [EmailAddress]
            [Display(Name = "Имейл")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "Полето '{0}' трябва да бъде между {2} и максимум {1} символа.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Парола")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Потвърди паролата")]
            [Compare("Password", ErrorMessage = "Паролата и потвърди паролата не съвпадат")]
            public string ConfirmPassword { get; set; }

            [Required]
            [Display(Name = "Име")]
            [StringLength(30, ErrorMessage = "Полето '{0}' трябва да бъде между {2} и максимум {1} символа.", MinimumLength = 3)]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Фамилия")]
            [StringLength(30, ErrorMessage = "Полето '{0}' трябва да бъде между {2} и максимум {1} символа.", MinimumLength = 3)]
            public string LastName { get; set; }

            [Required]
            [Display(Name = "Фирма")]
            [StringLength(30, ErrorMessage = "Полето '{0}' трябва да бъде между {2} и максимум {1} символа.", MinimumLength = 3)]
            public string Company { get; set; }

            [Required]
            [Display(Name = "Длъжност")]
            [Range(1, 10, ErrorMessage = "Избери длъжност")]
            public int PositionId { get; set; }

            [Required]
            [Display(Name = "Град")]
            [Range(1, 10, ErrorMessage = "Избери град")]
            public int TownId { get; set; }

            [Required]
            [Display(Name = "Телефон")]
            [StringLength(30, ErrorMessage = "Полето '{0}' трябва да бъде между {2} и максимум {1} символа.", MinimumLength = 3)]
            public string PhoneNumber { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            this.ReturnUrl = returnUrl;
            this.ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = this.Input.Email,
                    Email = this.Input.Email,
                    FirstName = this.Input.FirstName,
                    LastName = this.Input.LastName,
                    PasswordHash = this.Input.Password,
                    PositionId = this.Input.PositionId,
                    TownId = this.Input.TownId,
                    PhoneNumber = this.Input.PhoneNumber,
                };

                var result = await userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    logger.LogInformation("User created a new account with password.");

                    var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
