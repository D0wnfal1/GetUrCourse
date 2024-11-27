using GetUrCourse.Services.UserAPI.Application.UseCases.Users.Commands.Create;
using GetUrCourse.Services.UserAPI.Application.UseCases.Users.Commands.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GetUrCourse.Services.UserAPI.Controllers;
[Route("api/members")]
public class UserController : ApiController
{
    public UserController(ISender sender) : base(sender)
    {
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
    {
        var result = await Sender.Send(command);
        return result.IsSuccess ? Ok(result.Value) : HandleFailure(result);
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateUser( [FromBody] UpdateUserCommand command)
    {
        var result = await Sender.Send(command);
        return result.IsSuccess ? Ok() : HandleFailure(result);
    }
    
    
}