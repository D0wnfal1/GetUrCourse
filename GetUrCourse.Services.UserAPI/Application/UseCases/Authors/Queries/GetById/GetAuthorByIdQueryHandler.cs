using GetUrCourse.Services.UserAPI.Application.Messaging;
using GetUrCourse.Services.UserAPI.Core.Shared;
using GetUrCourse.Services.UserAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Authors.Queries.GetById;

public class GetAuthorByIdQueryHandler(UserDbContext context) : IQueryHandler<GetAuthorByIdQuery, AuthorMainResponse>
{
    public async Task<Result<AuthorMainResponse>> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
    {
        var author = await context.Authors
            .AsNoTracking()
            .Where(a => a.UserId == request.Id)
            .Include(a => a.User)
            .Select(a => new AuthorMainResponse(
                a.UserId,
                a.User.ImageUrl ?? string.Empty,
                a.User.Name,
                a.User.Bio, 
                a.TotalStudents, 
                a.TotalReviews,
                a.User.SocialLinks,
                a.AverageRating))
            .FirstOrDefaultAsync(cancellationToken);
        
        if (author is null )
        {
            return Result.Failure<AuthorMainResponse>(
                new Error("get_author","Problem with getting author from database"));
        }
         
        return Result.Success(author);
    }
    
}