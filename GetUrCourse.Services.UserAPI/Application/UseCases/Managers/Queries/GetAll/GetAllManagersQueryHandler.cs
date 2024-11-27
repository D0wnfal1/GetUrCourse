using GetUrCourse.Services.UserAPI.Application.Messaging;
using GetUrCourse.Services.UserAPI.Core.Models;
using GetUrCourse.Services.UserAPI.Core.Shared;
using GetUrCourse.Services.UserAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Managers.Queries.GetAll;

public class GetAllManagersQueryHandler(UserDbContext context) : IQueryHandler<GetAllManagersQuery, PagedList<ManagerResponse>>
{
    public async Task<Result<PagedList<ManagerResponse>>> Handle(GetAllManagersQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Manager> managersQuery = context.Managers
            .AsNoTracking()
            .Include(c => c.User);
        
        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            managersQuery = managersQuery.Where(c =>
                c.User.Name.FirstName.Contains(request.SearchTerm) || 
                c.User.Name.LastName.Contains(request.SearchTerm));
        }
        
        if (request.Department is not null)
        {
            managersQuery = managersQuery.Where(c =>
                c.Department == request.Department);
        }
        

        var managersResponseQuery = managersQuery
            .Select(c => new ManagerResponse(
                c.UserId,
                c.User.ImageUrl.ToString(),
                c.User.Name.ToString(),
                c.User.Email,
                c.Department));
        
        var result = await PagedList<ManagerResponse>
            .CreateAsync(managersResponseQuery, request.PageNumber, request.PageSize);

        if (result.IsFailure)
        {
            return Result.Failure<PagedList<ManagerResponse>>(
                new Error("get_managers",result.Error));
        }
        return Result.Success(result.Value!);
    }
}