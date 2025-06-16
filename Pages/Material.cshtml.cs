using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ClassroomManagement.Data;
using ClassroomManagement.Models;
using System.ComponentModel.DataAnnotations;
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

    [BindProperty(SupportsGet = true)]
    public bool EditMode { get; set; } = false;

    public CourseMaterial? Material { get; set; }

    // For editing
    [BindProperty]
    [Required]
    public string Title { get; set; } = "";

    [BindProperty]
    [Required]
    public string MaterialContent { get; set; } = ""; // Renamed from Content

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
        IsInstructor = User.IsInRole("Instructor") && user?.Id == Material.InstructorId;

        // Pre-fill for edit
        Title = Material.Title;
        MaterialContent = Material.Content;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var material = await _context.CourseMaterials
            .Include(m => m.Files)
            .FirstOrDefaultAsync(m => m.Id == Id);
        if (material == null)
            return NotFound();

        var user = await _userManager.GetUserAsync(User);
        var isInstructor = User.IsInRole("Instructor") && user?.Id == material.InstructorId;
        if (!isInstructor)
            return Forbid();

        if (!ModelState.IsValid)
        {
            Material = material;
            IsInstructor = isInstructor;
            EditMode = true;
            return Page();
        }

        material.Title = Title;
        material.Content = MaterialContent;
        await _context.SaveChangesAsync();

        return RedirectToPage("/Material", new { id = Id });
    }

    public async Task<IActionResult> OnPostDeleteAsync()
    {
        var material = await _context.CourseMaterials.FindAsync(Id);
        if (material == null)
            return NotFound();

        var user = await _userManager.GetUserAsync(User);
        var isInstructor = User.IsInRole("Instructor") && user?.Id == material.InstructorId;
        if (!isInstructor)
            return Forbid();

        var courseId = material.CourseId;
        _context.CourseMaterials.Remove(material);
        await _context.SaveChangesAsync();

        return RedirectToPage("/Course", new { id = courseId });
    }
}