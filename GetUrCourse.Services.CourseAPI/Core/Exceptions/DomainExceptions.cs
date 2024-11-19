namespace GetUrCourse.Services.CourseAPI.Core.Exceptions;

public static class DomainExceptions
{
    public static string Empty(string name) => 
        $"'{name}' should not be empty!";
    public static string EmptyOrLonger(string name, int maxValue) => 
        $"'{name}' should not be empty or exceed {maxValue} characters!";
    public static string MaxLength(string name, int maxValue) => 
        $"'{name}' should not exceed {maxValue} characters!";
    
}