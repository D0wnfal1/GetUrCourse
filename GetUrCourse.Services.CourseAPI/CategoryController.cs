using GetUrCourse.Services.CourseAPI.Application.UseCases.Categories.Commands.Create;
using GetUrCourse.Services.CourseAPI.Application.UseCases.Categories.Commands.Delete;
using GetUrCourse.Services.CourseAPI.Application.UseCases.Categories.Commands.Update;
using GetUrCourse.Services.CourseAPI.Application.UseCases.Categories.Queries.GetBase;
using GetUrCourse.Services.CourseAPI.Application.UseCases.Categories.Queries.GetChildrenById;
using GetUrCourse.Services.CourseAPI.Application.UseCases.Comments.Commands.Update;
using GetUrCourse.Services.CourseAPI.Application.UseCases.Courses.Commands.AddComment;
using GetUrCourse.Services.CourseAPI.Application.UseCases.Courses.Commands.Create;
using GetUrCourse.Services.CourseAPI.Application.UseCases.Courses.Commands.CreateCourse;
using GetUrCourse.Services.CourseAPI.Application.UseCases.Students.Commands.AddSubscription;
using GetUrCourse.Services.CourseAPI.Application.UseCases.Students.Commands.Create;
using GetUrCourse.Services.CourseAPI.Application.UseCases.Subscriptions.Commands.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GetUrCourse.Services.CourseAPI;
[Route("api/[controller]")]
public class CategoryController(ISender sender) : ApiController(sender)
{
    [HttpPost]
    public async Task<IActionResult> CreateCategory(
        CreateCategoryCommand command)
    {
        var result = await Sender.Send(command);

        return result.IsSuccess
            ? Ok()
            : HandleFailure(result);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetBaseCategories(
        [FromQuery] GetBaseCategoriesQuery query)
    {
        var result = await Sender.Send(query);

        return result.IsSuccess
            ? Ok(result.Value)
            : HandleFailure(result);
    }
    
    [HttpGet("children/{id}")]
    public async Task<IActionResult> GetCategoryChildrenById(
        [FromRoute] Guid id)
    {
        var result = await Sender.Send(new GetChildrenCategoriesByIdQuery(id));

        return result.IsSuccess
            ? Ok(result.Value)
            : HandleFailure(result);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(
        UpdateCategoryByIdCommand command)
    {
        var result = await Sender.Send(command);

        return result.IsSuccess
            ? Ok()
            : HandleFailure(result);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(
        [FromRoute] Guid id)
    {
        var result = await Sender.Send(new DeleteCategoryByIdCommand(id));

        return result.IsSuccess
            ? Ok()
            : HandleFailure(result);
    }
    
    [HttpPost("add-course")]
    public async Task<IActionResult> AddCourse(
        CreateCourseCommand command)
    {
        var result = await Sender.Send(command);

        return result.IsSuccess
            ? Ok()
            : HandleFailure(result);
    }
    
    [HttpPost("add-student")]
    public async Task<IActionResult> AddStudent(
        CreateStudentCommand command)
    {
        var result = await Sender.Send(command);

        return result.IsSuccess
            ? Ok()
            : HandleFailure(result);
    }
    
    [HttpPost("add-subscription")]
    public async Task<IActionResult> AddSubscription(
        CreateSubscriptionCommand command)
    {
        var result = await Sender.Send(command);

        return result.IsSuccess
            ? Ok()
            : HandleFailure(result);
    }
    
    
    [HttpPost("add-subscription-to-student")]
    public async Task<IActionResult> AddSubscriptionToStudent(
        AddSubscriptionToStudentCommand command)
    {
        var result = await Sender.Send(command);

        return result.IsSuccess
            ? Ok()
            : HandleFailure(result);
    }
    
    [HttpPost("add-comment-to-course")]
    public async Task<IActionResult> AddCommentToCourse(
        CreateCommentCommand command)
    {
        var result = await Sender.Send(command);

        return result.IsSuccess
            ? Ok()
            : HandleFailure(result);
    }
    
    [HttpPut("update-comment")]
    public async Task<IActionResult> UpdateComment(
        UpdateCommentCommand command)
    {
        var result = await Sender.Send(command);

        return result.IsSuccess
            ? Ok()
            : HandleFailure(result);
    }
    
    
    
}