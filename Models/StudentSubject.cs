namespace ClassroomManagement.Models;
// public class StudentSubject
// {
//     public int StudentId { get; set; }
//     public Student Student { get; set; }

//     public int SubjectId { get; set; }
//     public Subject Subject { get; set; }
// }

public class StudentSubject
{
    public string StudentId { get; set; }
    public ApplicationUser Student { get; set; } = null!;

    public int SubjectId { get; set; }
    public Subject Subject { get; set; } = null!;
}
