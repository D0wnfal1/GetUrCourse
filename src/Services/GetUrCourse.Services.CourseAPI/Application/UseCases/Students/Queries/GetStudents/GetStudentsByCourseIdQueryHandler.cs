using GetUrCourse.Services.CourseAPI.Application.Messaging;
using GetUrCourse.Services.CourseAPI.Core.Models;
using GetUrCourse.Services.CourseAPI.Infrastructure.Data;
using GetUrCourse.Services.CourseAPI.Shared;
using Microsoft.EntityFrameworkCore;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Students.Queries.GetStudents;

public class GetStudentsByCourseIdQueryHandler(CourseDbContext context) : IQueryHandler<GetStudentsByCourseIdQuery, PagedList<StudentResponse>>
{
    public async Task<Result<PagedList<StudentResponse>>> Handle(GetStudentsByCourseIdQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Student> studentsQuery = context.Students
            .AsNoTracking()
            .Where(s => s.Courses.Any(c => c.Id == request.CourseId));

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            studentsQuery = studentsQuery
                .Where(s => s.FullName.Contains(request.SearchTerm));
        }

        var students = studentsQuery
            .Select(s => new StudentResponse(
                s.Id,
                s.FullName,
                s.ImageUrl));
        
        var result = await PagedList<StudentResponse>.CreateAsync(
            students,
            request.PageNumber,
            request.PageSize);
        
        if (result.IsFailure)
            return Result.Failure<PagedList<StudentResponse>>(
                new Error("students_not_found", "Students not found"));
        
        return Result.Success(result.Value!);
    }
}