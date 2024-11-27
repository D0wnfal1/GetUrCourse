using GetUrCourse.Services.UserAPI.Application.Messaging;
using GetUrCourse.Services.UserAPI.Core.Shared;
using GetUrCourse.Services.UserAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Managers.Queries.GetById;

public class GetManagerByIdQueryHandler(UserDbContext context) : IQueryHandler<GetManagerByIdQuery, ManagerResponse>
{
    public async Task<Result<ManagerResponse>> Handle(GetManagerByIdQuery request, CancellationToken cancellationToken)
    {
        var manager = await context.Managers
            .AsNoTracking()
            .Where(m => m.UserId == request.Id)
            .Include(m => m.User)
            .Select(m => new ManagerResponse(
                m.UserId,
                m.User.ImageUrl.ToString(),
                m.User.Name.ToString(),
                m.User.Email,
                m.Department))
            .FirstOrDefaultAsync(cancellationToken);
        
        if (manager is null )
        {
            return Result.Failure<ManagerResponse>(
                new Error("get_manager","Manager not found"));
        }
         
        return Result.Success(manager);
    }
}