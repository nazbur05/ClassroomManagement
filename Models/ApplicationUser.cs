using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ClassroomManagement.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public string Specialization { get; set; } = string.Empty;

        public List<Course>? CoursesTaught { get; set; }
        public List<StudentCourse>? StudentCourses { get; set; }
    }
}
