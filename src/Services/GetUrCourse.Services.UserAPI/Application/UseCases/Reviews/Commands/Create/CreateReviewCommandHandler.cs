using GetUrCourse.Services.UserAPI.Application.Messaging;
using GetUrCourse.Services.UserAPI.Core.Models;
using GetUrCourse.Services.UserAPI.Core.Shared;
using GetUrCourse.Services.UserAPI.Infrastructure.Data;

namespace GetUrCourse.Services.UserAPI.Application.UseCases.Reviews.Commands.Create;

public class CreateReviewCommandHandler(UserDbContext context) : ICommandHandler<CreateReviewCommand>
{
    public async Task<Result> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        var review = Review.Create(
            request.AuthorId,
            request.StudentId,
            request.Text,
            request.Rating);
        if (review.IsFailure)
        {
            return Result.Failure(review.Error);
        }
        
        await context.Reviews.AddAsync(review.Value, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}