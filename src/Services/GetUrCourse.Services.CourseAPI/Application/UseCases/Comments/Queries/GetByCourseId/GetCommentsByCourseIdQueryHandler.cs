using GetUrCourse.Services.CourseAPI.Application.Messaging;
using GetUrCourse.Services.CourseAPI.Infrastructure.Data;
using GetUrCourse.Services.CourseAPI.Shared;
using Microsoft.EntityFrameworkCore;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Comments.Queries.GetByCourseId;

public class GetCommentsByCourseIdQueryHandler(CourseDbContext context) : IQueryHandler<GetCommentsByCourseIdQuery, PagedList<CourseCommentResponse>>
{
    public async Task<Result<PagedList<CourseCommentResponse>>> Handle(GetCommentsByCourseIdQuery request, CancellationToken cancellationToken)
    {
        var commentQuery = context.CourseComments
            .Where(c => c.CourseId == request.Id)
            .Include(cc => cc.Student)
            .Select(c => new CourseCommentResponse(
                c.Id,
                c.Text,
                c.Rating,
                c.CourseId,
                c.StudentId,
                c.Student.FullName,
                c.CreatedAt));
        
        var result = await PagedList<CourseCommentResponse>
            .CreateAsync(commentQuery, request.PageNumber, request.PageSize);
        
        if (result.IsFailure)
            return Result.Failure<PagedList<CourseCommentResponse>>(new Error(
                "get_comments_by_course_id", "Problem with getting comments by course id"));
        
        return Result.Success(result.Value!);
    }
}