using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClassroomManagement.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassroomManagement.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class UsersModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public List<ApplicationUser> Instructors { get; set; }
        public List<ApplicationUser> Students { get; set; }

        public UsersModel(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [BindProperty]
        public string NewEmail { get; set; }
        [BindProperty]
        public string NewFirstName { get; set; }
        [BindProperty]
        public string NewLastName { get; set; }
        [BindProperty]
        public string NewPassword { get; set; }
        [BindProperty]
        public string NewRole { get; set; }

        public async Task OnGetAsync()
        {
            var users = _userManager.Users.ToList();

            Instructors = new List<ApplicationUser>();
            Students = new List<ApplicationUser>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                if (roles.Contains("Instructor")) Instructors.Add(user);
                if (roles.Contains("Student")) Students.Add(user);
            }
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            var user = new ApplicationUser
            {
                UserName = NewEmail,
                Email = NewEmail,
                FirstName = NewFirstName,
                LastName = NewLastName,
                EmailConfirmed = true
            };
            var result = await _userManager.CreateAsync(user, NewPassword);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, NewRole);
            }
            return RedirectToPage("/Main");
        }

        public async Task<IActionResult> OnPostDeleteAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
            return RedirectToPage();
        }
    }
}