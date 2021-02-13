using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using NowDelivary.Data;
using NowDelivary.Models;

namespace NowDelivary.RazorPages
{
    public class DelivarymanRazorPageModel : PageModel
    {
        private readonly SignInManager<CustomUser> _signInManager;
        private readonly UserManager<CustomUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<DelivarymanRazorPageModel> _logger;
        private readonly IEmailSender _emailSender;
        private ApplicationDbContext _context;

        public DelivarymanRazorPageModel(
            UserManager<CustomUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<CustomUser> signInManager,
            ILogger<DelivarymanRazorPageModel> logger,
            IEmailSender emailSender,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
            _roleManager = roleManager;
            Area = _context.Area.Select(a => new SelectListItem { Value=a.ID.ToString(),Text=a.AreaName}).ToList();
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [DisplayName("Delivaryman Name")]
            public string UserName { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Display(Name = "Address Description")]
            [Required]
            public string Description { get; set; }

            [Display(Name = "Address Special Mark")]
            [Required]
            public string SpescialMark { get; set; }

            [Required]
            [Display(Name = "Identity Number")]
            public int IdentityNumber { get; set; }

            public int areaID { get; set; }

            [Required]
            [RegularExpression("^01[0-2][0-9]{8}$", ErrorMessage = "This Phone Number is not correct")]
            public string PhoneNumber { get; set; }
        }

        public List<SelectListItem> Area { get; set; }

        public async Task OnGet(string returnUrl = null)
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
                var user = new CustomUser { UserName = Input.UserName, Email = Input.Email,IdentityNumber=Input.IdentityNumber, PhoneNumber = Input.PhoneNumber };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("Delivaryman created a new account with password.");

                    var address = new CustomerAddress() { CustomerID = user.Id, Description = Input.Description, SpecialMark = Input.SpescialMark, AreaID = Input.areaID };
                    _context.CustomerAddresse.Add(address);
                    _context.SaveChanges();

                    if (!await _roleManager.RoleExistsAsync("Delivaryman"))
                        await _roleManager.CreateAsync(new IdentityRole("Delivaryman"));

                    await _userManager.AddToRoleAsync(user, "Delivaryman");
                    //await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
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