using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

public class DashboardModel : PageModel
{
    public string UserName { get; set; } = "Yessen G.";

    public Dictionary<string, string> Translations { get; set; }

    public List<Course> Courses { get; set; }

    public void OnGet()
    {
        Translations = new Dictionary<string, string>
        {
            { "dashboard", "Dashboard" },
            { "profile", "Profile" },
            { "courses", "My Courses" },
            { "tasks", "Tasks" },
            { "settings", "Settings" },
            { "myCourses", "My Courses" }
        };

        Courses = new List<Course>
        {
            new Course { Title = "Health Education in Lifestyle Diseases", Code = "GP: 2LID-A-CPS/2023-ASK01", Image = "https://st2.depositphotos.com/2250467/7841/v/450/depositphotos_78410298-stock-illustration-medical-icons-for-web-and.jpg" },
            new Course { Title = "Social Problems of IT", Code = "GP: 2LID-A-ERASMUS/2023-ASK01", Image = "https://news.workforce.com/wp-content/uploads/sites/2/2018/12/AI-is-coming-%E2%80%94-and-HR-is-not-prepared.jpg" },
            new Course { Title = "Computer System Architecture", Code = "GP: 4LID-A-GDD/2023-ASL01", Image = "https://media.designrush.com/articles/231707/conversions/information-technology-preview.jpg" },
            new Course { Title = "Software Engineering", Code = "GP: 4LID-A-P/2023-ASL01", Image = "https://studenthub.africa/app/uploads/blog/DgmQowf_JLkY2wy8JJWgt3KZbqK2bYk6.jpeg" },
            new Course { Title = "Programming", Code = "GP: 1LID-A-ERASMUS/2024-ASL01", Image = "https://media.licdn.com/dms/image/v2/D5612AQESyQkhP_NCuw/article-cover_image-shrink_720_1280/article-cover_image-shrink_720_1280/0/1711312586086?e=2147483647&v=beta&t=ikf0VZw5RNQQ5T9VL_Z9ya19rhjbi65hml9cEmPsq2Q" }
        };
    }

    public class Course
    {
        public string Title { get; set; }
        public string Code { get; set; }
        public string Image { get; set; }
    }
}
