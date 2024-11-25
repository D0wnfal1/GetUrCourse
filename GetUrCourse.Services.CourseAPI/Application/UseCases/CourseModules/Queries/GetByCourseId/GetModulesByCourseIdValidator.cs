using FluentValidation;
using GetUrCourse.Services.CourseAPI.Infrastructure.Data;
using GetUrCourse.Services.CourseAPI.Shared;

namespace GetUrCourse.Services.CourseAPI.Application.UseCases.CourseModules.Queries.GetByCourseId;

public class GetModulesByCourseIdValidator : AbstractValidator<GetModulesByCourseIdQuery>
{
    public GetModulesByCourseIdValidator(CourseDbContext context) 
    {
        RuleFor(x => x.CourseId)
            .NotEmpty()
            .WithMessage("CourseId is required.")
            .IsCourseExist(context);
    }
}