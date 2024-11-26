using FluentValidation;
using GetUrCourse.Services.CourseAPI.Core.Models;
using GetUrCourse.Services.CourseAPI.Shared;

namespace GetUrCourse.Services.CourseAPI.Core.Validators;

public class CourseCommentValidator : AbstractValidator<CourseComment>
{
    public CourseCommentValidator()
    {
        RuleFor(c => c.Text)
            .NotEmptyAndNotLongerThan(
                nameof(CourseComment.Text),
                CourseComment.MaxCommentLength
            );
    }
}