namespace ClassroomManagement.Models;
public class Subject
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public string InstructorId { get; set; }
    public ApplicationUser Instructor { get; set; } = null!;

    public List<StudentSubject> StudentSubjects { get; set; } = new();
}
