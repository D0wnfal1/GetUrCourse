using FluentValidation;
using GetUrCourse.Services.CourseAPI.Core.Exceptions;
using GetUrCourse.Services.CourseAPI.Core.Models;
using GetUrCourse.Services.CourseAPI.Core.ValueObjects;
using GetUrCourse.Services.CourseAPI.Shared;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Comments.Commands.Update;

public class UpdateCommentValidator : AbstractValidator<UpdateCommentCommand>
{
    public UpdateCommentValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(DomainExceptions.Empty(nameof(UpdateCommentCommand.Id)));   
        
        RuleFor(x => x.Text)
            .NotEmptyAndNotLongerThan(nameof(CourseComment), CourseComment.MaxCommentLength);
        
        RuleFor(x => x.Rating)
            .MustBeValueObject(Rating.Create);
    }
}