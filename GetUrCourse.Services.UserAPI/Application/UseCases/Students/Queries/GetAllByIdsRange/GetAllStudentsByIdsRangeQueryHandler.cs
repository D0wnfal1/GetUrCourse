using GetUrCourse.Services.UserAPI.Application.Messaging;
using GetUrCourse.Services.UserAPI.Core.Shared;
using GetUrCourse.Services.UserAPI.Infrastructure.Data;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Students.Queries.GetAllByIdsRange;

public class GetAllStudentsByIdsRangeQueryHandler(UserDbContext context) : IQueryHandler<GetAllStudentsByIdsRangeQuery, PagedList<StudentShortResponse>>
{
    public async Task<Result<PagedList<StudentShortResponse>>> Handle(GetAllStudentsByIdsRangeQuery request, CancellationToken cancellationToken)
    {
        // Queryable<Student> reviewsQuery = context.Students
        //     .AsNoTracking()
        //     .Where(s => request.StudentsIds.Contains(s.UserId));
        //     
        //     
        //
        // reviewsQuery = request.SortAscending?.ToLower() == "desc" ? 
        //     reviewsQuery.OrderByDescending(GetSortProperty(request)) : 
        //     reviewsQuery.OrderBy(GetSortProperty(request));
        //
        // var reviewsResponseQuery = reviewsQuery
        //     .Select(r => new ReviewResponse(
        //         r.Id,
        //         r.StudentId,
        //         r.Student.User.ImageUrl.ToString(),
        //         r.Student.User.Name.ToString(),
        //         r.Text,
        //         r.Rating,
        //         r.CreatedAt));
        //
        // var result = await PagedList<ReviewResponse>
        //     .CreateAsync(reviewsResponseQuery, request.PageNumber, request.PageSize);
        return Result.Failure<PagedList<StudentShortResponse>>(new Error("esdv", "vsdv"));
    }
}