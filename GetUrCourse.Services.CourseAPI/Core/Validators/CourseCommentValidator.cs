using FluentValidation;
using GetUrCourse.Services.CourseAPI.Core.Exceptions;
using GetUrCourse.Services.CourseAPI.Core.Models;

namespace GetUrCourse.Services.CourseAPI.Core.Validators;

public class CourseCommentValidator : AbstractValidator<CourseComment>
{
    public CourseCommentValidator()
    {
        RuleFor(c => c.Text)
            .NotEmpty()
            .WithMessage(DomainExceptions.Empty(nameof(CourseComment.Text)))
            .MaximumLength(CourseComment.MaxCommentLength)
            .WithMessage(DomainExceptions.MaxLength(nameof(CourseComment.Text), CourseComment.MaxCommentLength));
    }
}