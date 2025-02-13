using GetUrCourse.Services.UserAPI.Application.Messaging;
using GetUrCourse.Services.UserAPI.Core.Enums;
using GetUrCourse.Services.UserAPI.Core.Shared;
using GetUrCourse.Services.UserAPI.Core.ValueObjects;
using GetUrCourse.Services.UserAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Users.Queries.GetById;

public class GetUserByIdQueryHandler(UserDbContext context) : IQueryHandler<GetUserByIdQuery, UserFullResponse>
{
    public async Task<Result<UserFullResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await context.Users
            .AsNoTracking()
            .Where(u => u.Id == request.Id)
            .Select(u => new UserFullResponse(
                u.Id,
               u.ImageUrl != null ? u.ImageUrl.ToString() : string.Empty,
                u.Name,
                u.Email,
                u.Birthday ?? BirthdayDate.Empty,
                u.Sex ?? Sex.NotSpecified,
                u.Location ?? Location.Empty,
                u.Bio ?? string.Empty,
                u.SocialLinks ?? SocialLinks.Empty))
            .FirstOrDefaultAsync(cancellationToken);

        if (user is null)
        {
            return Result.Failure<UserFullResponse>(new Error(
                "get_user_byid",
                "Problem with getting user from database"));
        }

        return Result.Success(user);

    }
}