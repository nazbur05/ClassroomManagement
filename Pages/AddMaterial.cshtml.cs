using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClassroomManagement.Data;
using ClassroomManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

public class AddMaterialModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public AddMaterialModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [BindProperty(SupportsGet = true)]
    public int CourseId { get; set; }

    [BindProperty]
    public string Title { get; set; }
    [BindProperty]
    public string Content { get; set; }
    [BindProperty]
    public IFormFile? Upload { get; set; } // for file upload

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        var instructorId = _userManager.GetUserId(User);

        var material = new CourseMaterial
        {
            CourseId = CourseId,
            Title = Title,
            Content = Content,
            InstructorId = instructorId
            // Add handling file upload soon
        };

        _context.CourseMaterials.Add(material);
        await _context.SaveChangesAsync();

        return RedirectToPage("/Course", new { id = CourseId });
    }
}