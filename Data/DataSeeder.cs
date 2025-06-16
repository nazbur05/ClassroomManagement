using ClassroomManagement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ClassroomManagement.Data
{
    public static class DataSeeder
    {
        public static async Task SeedInstructors(UserManager<ApplicationUser> userManager)
        {
            var instructors = new[]
            {
                new { User = new ApplicationUser
                        {
                            UserName = "mjagiela@classroom.com",
                            Email = "mjagiela@classroom.com",
                            FirstName = "Marcin",
                            LastName = "Jagiela",
                            EmailConfirmed = true
                        },
                      Password = "Mjagielapswd1"
                },
                new { User = new ApplicationUser
                        {
                            UserName = "mknap@classroom.com",
                            Email = "mknap@classroom.com",
                            FirstName = "Maksymilian",
                            LastName = "Knap",
                            EmailConfirmed = true
                        },
                      Password = "Mknappswd1"
                }
            };

            foreach (var instructorData in instructors)
            {
                var user = await userManager.FindByEmailAsync(instructorData.User.Email);
                if (user == null)
                {
                    var result = await userManager.CreateAsync(instructorData.User, instructorData.Password);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(instructorData.User, "Instructor");
                    }
                    else
                    {
                        Console.WriteLine($"Failed to create user {instructorData.User.Email}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                    }
                }
            }
        }

        public static async Task SeedCourses(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            await context.Database.EnsureCreatedAsync();

            // Get instructors seeded earlier
            var instructor1 = await userManager.Users.FirstOrDefaultAsync(u => u.Email == "mjagiela@classroom.com");
            var instructor2 = await userManager.Users.FirstOrDefaultAsync(u => u.Email == "mknap@classroom.com");

            if (instructor1 == null || instructor2 == null)
            {
                throw new Exception("Instructors must be seeded before courses.");
            }

            if (!context.Courses.Any())
            {
                var courses = new[]
                {
                    new Course
                    {
                        Name = "Programming Languages",
                        InstructorId = instructor1.Id,
                        Description = "Basics of .NET"
                    },
                    new Course
                    {
                        Name = "Software Engineering",
                        InstructorId = instructor2.Id,
                        Description = "All about the Software Development cycle"
                    }
                };

                context.Courses.AddRange(courses);
                await context.SaveChangesAsync();
            }

            // Assign CourseMaterials if not already present
            if (!context.CourseMaterials.Any())
            {
                var introCourse = await context.Courses.FirstOrDefaultAsync(c => c.Name == "Programming Languages");
                var algoCourse = await context.Courses.FirstOrDefaultAsync(c => c.Name == "Software Engineering");

                var materials = new[]
                {
                    new CourseMaterial
                    {
                        CourseId = introCourse.Id,
                        InstructorId = instructor1.Id,
                        Title = "Lecture 1: Getting Started",
                        Content = "Welcome to programming!",
                        FilePath = null
                    },
                    new CourseMaterial
                    {
                        CourseId = algoCourse.Id,
                        InstructorId = instructor2.Id,
                        Title = "Lecture 1: Types of requirements",
                        Content = "Will add .pdf file soon",
                        FilePath = null
                    }
                };

                context.CourseMaterials.AddRange(materials);
                await context.SaveChangesAsync();
            }
        }

        public static async Task SeedStudentCourses(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            await context.Database.EnsureCreatedAsync();

            // Get students by email
            var student1 = await userManager.Users.FirstOrDefaultAsync(u => u.Email == "gulzatyessen@gmail.com");
            var student2 = await userManager.Users.FirstOrDefaultAsync(u => u.Email == "nazburgenbay01@gmail.com");
            var student3 = await userManager.Users.FirstOrDefaultAsync(u => u.Email == "dilnazdosumbek7@gmail.com");

            if (student1 == null || student2 == null || student3 == null)
            {
                throw new Exception("Students must be seeded before assigning courses.");
            }

            // Get courses by name (use the same names as in SeedCourses)
            var course1 = await context.Courses.FirstOrDefaultAsync(c => c.Name == "Programming Languages");
            var course2 = await context.Courses.FirstOrDefaultAsync(c => c.Name == "Software Engineering");

            if (course1 == null || course2 == null)
            {
                throw new Exception("Courses must be seeded before assigning students.");
            }

            // Only seed if not already present
            if (!context.StudentCourses.Any())
            {
                var studentCourses = new[]
                {
                    // Enroll all three students in both courses as an example
                    new StudentCourse { StudentId = student1.Id, CourseId = course1.Id },
                    new StudentCourse { StudentId = student1.Id, CourseId = course2.Id },
                    new StudentCourse { StudentId = student2.Id, CourseId = course1.Id },
                    new StudentCourse { StudentId = student2.Id, CourseId = course2.Id },
                    new StudentCourse { StudentId = student3.Id, CourseId = course1.Id },
                    new StudentCourse { StudentId = student3.Id, CourseId = course2.Id },
                };

                context.StudentCourses.AddRange(studentCourses);
                await context.SaveChangesAsync();
            }
        }
    }
}