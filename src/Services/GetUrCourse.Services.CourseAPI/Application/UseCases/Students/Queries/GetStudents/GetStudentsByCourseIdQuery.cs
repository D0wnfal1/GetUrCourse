using GetUrCourse.Services.CourseAPI.Application.Messaging;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Students.Queries.GetStudents;

public record GetStudentsByCourseIdQuery(
    Guid CourseId, 
    string? SearchTerm,
    int PageNumber,
    int PageSize) : IQuery<PagedList<StudentResponse>>;
