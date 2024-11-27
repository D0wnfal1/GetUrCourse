using GetUrCourse.Services.UserAPI.Application.Messaging;
using GetUrCourse.Services.UserAPI.Core.Shared;
using GetUrCourse.Services.UserAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Users.Queries.GetByEmail;

public class GetUserByEmailQueryHandler(UserDbContext context) : IQueryHandler<GetUserByEmailQuery, UserShortResponse>
{
    public async Task<Result<UserShortResponse>> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
    {
        var user = await context.Users
            .AsNoTracking()
            .Where(u => u.Email == request.Email)
            .Select(u => new UserShortResponse(
                u.Id,
                u.ImageUrl.ToString(),
                u.Name.ToString()))
            .FirstOrDefaultAsync(cancellationToken);

        if (user is null)
        {
            return Result.Failure<UserShortResponse>(new Error(
                "get_user_byid",
                "Problem with getting user from database"));
        }

        return Result.Success(user);
    }
}