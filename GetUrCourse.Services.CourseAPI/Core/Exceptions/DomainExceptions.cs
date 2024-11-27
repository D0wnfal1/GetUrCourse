using GetUrCourse.Services.CourseAPI.Shared;

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

public static class ValidationErrors
{
    public static Error Empty(string name) => 
        new (name, $"'{name}' should not be empty!");

    public static Error EmptyOrLonger(string name, int maxValue) =>
        new(name, $"'{name}' should not be empty or exceed {maxValue} characters!");
    public static Error MaxLength(string name, int maxValue) => 
        new(name, $"'{name}' should not exceed {maxValue} characters!");
    
}