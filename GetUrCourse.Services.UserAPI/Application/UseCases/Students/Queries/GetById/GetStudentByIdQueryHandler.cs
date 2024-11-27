using GetUrCourse.Services.UserAPI.Application.Messaging;
using GetUrCourse.Services.UserAPI.Core.Enums;
using GetUrCourse.Services.UserAPI.Core.Shared;
using GetUrCourse.Services.UserAPI.Core.ValueObjects;
using GetUrCourse.Services.UserAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Students.Queries.GetById;

public class GetStudentByIdQueryHandler(UserDbContext context): IQueryHandler<GetStudentByIdQuery, StudentFullResponse>
{
    public async Task<Result<StudentFullResponse>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
    {
        var student = await context.Students
            .AsNoTracking()
            .Where(s => s.UserId == request.Id)
            .Include(s => s.User)
            .Select(s => new StudentFullResponse(
                s.UserId,
                s.User.ImageUrl.ToString(),
                s.User.Name.ToString(),
                s.User.Email,
                s.User.Sex ?? Sex.NotSpecified,
                s.User.Birthday ?? BirthdayDate.Empty,
                s.User.Bio ?? string.Empty,
                s.User.SocialLinks ?? SocialLinks.Empty))
            .FirstOrDefaultAsync(cancellationToken);

        if (student is null)
        {
            return Result.Failure<StudentFullResponse>(new Error(
                "get_student",
                "Student is empty. Problem with fetching data from database"));
        }

        return Result.Success(student);
    }
}