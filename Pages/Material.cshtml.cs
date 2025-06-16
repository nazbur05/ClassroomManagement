using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ClassroomManagement.Data;
using ClassroomManagement.Models;
using System.Threading.Tasks;
using System.Linq;

public class MaterialModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public MaterialModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }

    public CourseMaterial Material { get; set; }
    public bool IsInstructor { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        Material = await _context.CourseMaterials
            .Include(m => m.Files)
            .Include(m => m.Course)
            .FirstOrDefaultAsync(m => m.Id == Id);

        if (Material == null)
            return NotFound();

        var user = await _userManager.GetUserAsync(User);
        IsInstructor = User.IsInRole("Instructor") && user.Id == Material.InstructorId;

        return Page();
    }
}