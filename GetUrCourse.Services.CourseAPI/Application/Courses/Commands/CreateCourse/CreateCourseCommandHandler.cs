using CSharpFunctionalExtensions;
using FluentValidation;
using GetUrCourse.Services.CourseAPI.Application.Messaging;
using GetUrCourse.Services.CourseAPI.Core.Models;
using GetUrCourse.Services.CourseAPI.Infrastructure.Data;

namespace GetUrCourse.Services.CourseAPI.Application.Courses.Commands.CreateCourse;

public class CreateCourseCommandHandler(CourseDbContext context, IValidator<CreateCourseCommand> validator) : ICommandHandler<CreateCourseCommand>
{
    public async Task<Result> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var validation = await validator.ValidateAsync(request, cancellationToken);
        if (!validation.IsValid)
            return Result.Failure(validation.Errors.First().ErrorMessage);
                
        var course = Course.Create(
            request.Title, 
            request.Subtitle, 
            request.FullDescription,
            request.Requirements,
            request.ImageUrl,
            request.Price,
            request.DiscountPrice,
            request.Language,
            request.Level,
            request.HasHomeTask,
            request.HasPossibilityToContactTheTeacher,
            request.CategoryId);
        
        if (course.IsFailure)
        {
            return Result.Failure(course.Error);
        }

        context.Courses.Add(course.Value);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

    