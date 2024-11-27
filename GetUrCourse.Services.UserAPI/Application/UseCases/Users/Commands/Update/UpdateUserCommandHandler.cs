using GetUrCourse.Services.UserAPI.Application.Messaging;
using GetUrCourse.Services.UserAPI.Core.Shared;
using GetUrCourse.Services.UserAPI.Infrastructure.Data;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Users.Commands.Update;

public class UpdateUserCommandHandler(UserDbContext context) : ICommandHandler<UpdateUserCommand>
{
    public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await context.Users.FindAsync(request.Id);
        if (user is null)
        {
            return Result.Failure(new Error("update_user", "User not found"));
        }

        user.Update(
            request.ImageUrl,
            request.Name,
            request.Email,
            request.Birthday,
            request.Location,
            request.Sex,
            request.SocialLinks,
            request.Bio,
            request.Role);
        
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}