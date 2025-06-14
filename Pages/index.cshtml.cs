using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using ClassroomManagement.Models;

public class LoginModel : PageModel
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ILogger<LoginModel> _logger;

    public LoginModel(SignInManager<ApplicationUser> signInManager, ILogger<LoginModel> logger)
    {
        _signInManager = signInManager;
        _logger = logger;
    }

    [BindProperty]
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; } = string.Empty;

    [BindProperty]
    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    public void OnGet()
    {
        _logger.LogInformation("Login page visited.");
    }

    public async Task<IActionResult> OnPostAsync()
    {
        _logger.LogInformation("Login attempt for email: {Email}", Email);

        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Login attempt failed model validation for email: {Email}", Email);
            return Page();
        }

        var result = await _signInManager.PasswordSignInAsync(Email, Password, isPersistent: false, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            _logger.LogInformation("Login succeeded for email: {Email}", Email);
            return RedirectToPage("/Main");
        }
        else
        {
            _logger.LogWarning("Login failed for email: {Email}", Email);
        }

        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        return Page();
    }
}