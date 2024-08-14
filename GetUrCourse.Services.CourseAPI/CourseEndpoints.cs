using FluentValidation;
using GetUrCourse.Services.CourseAPI.Application.Courses.Commands.CreateCourse;
using MediatR;

namespace GetUrCourse.Services.CourseAPI;

public static class CourseEndpoints 
{
    public static void AddCourseEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPost("api-course/course", async (
            ISender sender, 
            CreateCourseCommand command, 
            IValidator<CreateCourseCommand> validator) =>
        {
            var validation = await validator.ValidateAsync(command);
            
            if (!validation.IsValid)
                return Results.BadRequest(validation.Errors);
 
            var result = await sender.Send(command); 
            return result.IsSuccess ? Results.Ok() : Results.BadRequest(result.Error);
        });
    }
    
}