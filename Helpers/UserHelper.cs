using System.Text.RegularExpressions;

namespace ClassroomManagement.Helpers
{
    public static class UserHelper
    {
        public static bool IsValidStudentId(string studId)
            => !string.IsNullOrWhiteSpace(studId) && Regex.IsMatch(studId, @"^w\d{5}$");

        public static bool IsAdmin(IEnumerable<string> roles)
            => roles.Contains("Admin");

        public static bool IsStudent(IEnumerable<string> roles)
            => roles.Contains("Student");

        public static bool IsInstructor(IEnumerable<string> roles)
            => roles.Contains("Instructor");
    }
}