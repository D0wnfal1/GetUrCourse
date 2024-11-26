using FluentValidation;
using GetUrCourse.Services.CourseAPI.Core.Models;
using GetUrCourse.Services.CourseAPI.Core.ValueObjects;
using GetUrCourse.Services.CourseAPI.Infrastructure.Data;
using GetUrCourse.Services.CourseAPI.Shared;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Courses.Commands.AddComment;

public class CreateCommentValidator : AbstractValidator<CreateCommentCommand>
{
    public CreateCommentValidator(CourseDbContext context)
    {
        RuleFor(x => x.CourseId)
            .NotEmpty().WithMessage("CourseId is required.")
            .IsCourseExist(context);

        RuleFor(x => x.StudentId)
            .NotEmpty().WithMessage("StudentId is required.")
            .IsStudentExist(context);

        RuleFor(x => x.Text)
            .NotEmptyAndNotLongerThan(nameof(CourseComment), CourseComment.MaxCommentLength);

        RuleFor(x => x.Rating)
            .MustBeValueObject(Rating.Create);
    }
}