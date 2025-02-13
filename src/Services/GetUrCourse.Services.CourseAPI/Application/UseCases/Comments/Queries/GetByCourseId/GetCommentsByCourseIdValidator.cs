using FluentValidation;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Comments.Queries.GetByCourseId;

public class GetCommentsByCourseIdValidator : AbstractValidator<GetCommentsByCourseIdQuery>
{
    public GetCommentsByCourseIdValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Course Id is required.");
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber must be greater than or equal to 1.");
        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize must be greater than or equal to 1.");
    }
}