using static LogitWebApp.Common.GlobalConstants;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using LogitWebApp.Data.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using LogitWebApp.Attributes.ModelValidationAttributes;

namespace LogitWebApp.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel : IValidatableObject
        {
            [Required(ErrorMessage = "Задължително поле!")]
            [EmailAddress(ErrorMessage ="Невалиден email!")]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required(ErrorMessage ="Задължително поле!")]
            [StringLength(50, ErrorMessage = "Дължината на паролата трябва да е между 6 и 50 символа", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Парола")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Повтори паролата")]
            [Compare("Password", ErrorMessage = "Паролите не съвпадат!")]
            public string ConfirmPassword { get; set; }

            [Required(ErrorMessage = "Задължително поле!")]
            [Display(Name = "Фирма")]
            public string CompanyName { get; set; }

            [Required(ErrorMessage = "Задължително поле!")]
            [Display(Name = "Адрес")]
            public string Address { get; set; }

            [Required(ErrorMessage = "Задължително поле!")]
            [RegularExpression(@"^\+359[0-9]{9}$")]
            [Display(Name = "Мобилен номер")]
            public string PhoneNumber { get; set; }

            [Display(Name = "Факс")]
            public long? Fax { get; set; }

            [Display(Name = "ДДС Номер")]
            [BulstatMustStartsWithBGAttribute]
            public string VatNumber { get; set; }

            [Required(ErrorMessage = "Задължително поле!")]
            [Display(Name = "ЕИК")]
            [RegularExpression(@"^[0-9]{9}$", ErrorMessage = "Булстата съдържа точно 9 на брой цифри!")] //117616084
            [ValidateBulstatAttribute]            
            public int Bulstat { get; set; }

            [Required(ErrorMessage = "Полето Управител не може да бъде празно!")]
            [RegularExpression(@"^[а-яА-Яa-zA-z]+ [а-яА-Яa-zA-z]+ [а-яА-Яa-zA-z]+$", ErrorMessage = "Имената на управителя трябва да започват с голяма буква на кирилица, последвана само от малки!")]
            [Display(Name = "Управител")]
            public string Manager { get; set; }

            [Display(Name = "Сайт")]
            public string Site { get; set; }

            public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
            {
                if (this.VatNumber != "BG" + this.Bulstat.ToString())
                {
                    yield return new ValidationResult("ДДС и ЕИК номера не съвпадат!", new List<string>() { "Bulstat", "VatNumber" });
                }
            }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    PhoneNumber = Input.PhoneNumber,
                    CompanyName = Input.CompanyName,
                    Address = Input.Address,
                    VatNumber = Input.VatNumber,
                    Bulstat = Input.Bulstat,
                    Manager = Input.Manager,
                    Site = Input.Site,
                    Fax = Input.Fax
                };

                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    //Add new registered User to User Role
                    await _userManager.AddToRoleAsync(user, User_RoleName);

                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
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