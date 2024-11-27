using FluentValidation;
using GetUrCourse.Services.CourseAPI.Core.Exceptions;
using GetUrCourse.Services.CourseAPI.Core.Models;
using GetUrCourse.Services.CourseAPI.Infrastructure.Data;
using GetUrCourse.Services.CourseAPI.Shared;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.Comments.Commands.Delete;

public class DeleteCommentValidator : AbstractValidator<DeleteCommentCommand>
{
    public DeleteCommentValidator(CourseDbContext context)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(DomainExceptions.Empty(nameof(CourseComment.Id)))
            .IsCourseCommentExist(context);
    }
}