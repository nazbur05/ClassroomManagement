using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClassroomManagement.Models;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

[Authorize]
public class SettingsModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public SettingsModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string StudId { get; set; }
    public string Specialization { get; set; }

    [BindProperty]
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [BindProperty]
    [DataType(DataType.Password)]
    public string CurrentPassword { get; set; }

    [BindProperty]
    [DataType(DataType.Password)]
    [MinLength(6)]
    public string NewPassword { get; set; }

    [BindProperty]
    [DataType(DataType.Password)]
    [Compare("NewPassword", ErrorMessage = "Passwords do not match.")]
    public string ConfirmPassword { get; set; }

    [TempData]
    public string StatusMessage { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return RedirectToPage("/Account/Login");

        FirstName = user.FirstName;
        LastName = user.LastName;
        StudId = user.StudId;
        Specialization = user.Specialization;
        Email = user.Email;

        return Page();
    }

    public async Task<IActionResult> OnPostChangeEmailAsync()
    {
        if (!ModelState.IsValid) return Page();

        var user = await _userManager.GetUserAsync(User);
        if (user == null) return RedirectToPage("/Account/Login");

        if (Email != user.Email)
        {
            var setEmailResult = await _userManager.SetEmailAsync(user, Email);
            if (!setEmailResult.Succeeded)
            {
                foreach (var error in setEmailResult.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
                return Page();
            }
            user.UserName = Email;
            await _userManager.UpdateAsync(user);
            StatusMessage = "Your email has been updated.";
            await _signInManager.RefreshSignInAsync(user);
        }
        else
        {
            StatusMessage = "No changes made.";
        }
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostChangePasswordAsync()
    {
        if (!ModelState.IsValid) return Page();

        var user = await _userManager.GetUserAsync(User);
        if (user == null) return RedirectToPage("/Account/Login");

        var changePasswordResult = await _userManager.ChangePasswordAsync(user, CurrentPassword, NewPassword);
        if (!changePasswordResult.Succeeded)
        {
            foreach (var error in changePasswordResult.Errors)
                ModelState.AddModelError(string.Empty, error.Description);
            return Page();
        }

        await _signInManager.RefreshSignInAsync(user);
        StatusMessage = "Your password has been changed.";

        CurrentPassword = NewPassword = ConfirmPassword = "";

        return RedirectToPage();
    }
}