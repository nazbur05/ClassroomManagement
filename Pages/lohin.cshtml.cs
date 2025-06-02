using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

public class LoginModel : PageModel
{
    [BindProperty]
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [BindProperty]
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // TODO: Authenticate user (replace with real logic)
        if (Email == "test@example.com" && Password == "password123")
        {
            return RedirectToPage("/Dashboard"); // Example target after login
        }

        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        return Page();
    }
}
