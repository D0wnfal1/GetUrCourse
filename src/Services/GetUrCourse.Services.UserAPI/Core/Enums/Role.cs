namespace GetUrCourse.Services.UserAPI.Core.Enums;

[Flags]
public enum Role
{
    Admin = 1,
    Student = 2,
    Teacher = 4,
    Manager = 8
}