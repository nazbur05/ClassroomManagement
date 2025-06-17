using ClassroomManagement.Helpers;
using Xunit;

namespace ClassroomManagement.Tests.Helpers;

public class UserHelperTests
{
    [Theory]
    [InlineData("w12345", true)]
    [InlineData("w00000", true)]
    [InlineData("w1234", false)]   // only 4 digits
    [InlineData("12345", false)]   // missing 'w'
    [InlineData("w123456", false)] // too many digits
    [InlineData("", false)]        // empty
    [InlineData("w12a45", false)]  // letter in digits
    [InlineData(null, false)]      // null
    public void IsValidStudentId_WorksAsExpected(string input, bool expected)
    {
        Assert.Equal(expected, UserHelper.IsValidStudentId(input));
    }

    [Fact]
    public void IsAdmin_Works()
    {
        Assert.True(UserHelper.IsAdmin(new[] { "Admin" }));
        Assert.False(UserHelper.IsAdmin(new[] { "Student" }));
    }

    [Fact]
    public void IsStudent_Works()
    {
        Assert.True(UserHelper.IsStudent(new[] { "Student" }));
        Assert.False(UserHelper.IsStudent(new[] { "Instructor" }));
    }

    [Fact]
    public void IsInstructor_Works()
    {
        Assert.True(UserHelper.IsInstructor(new[] { "Instructor", "SomethingElse" }));
        Assert.False(UserHelper.IsInstructor(new[] { "Student" }));
    }
}