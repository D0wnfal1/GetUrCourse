using GetUrCourse.Services.ChatAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace GetUrCourse.Services.ChatAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessagesController(IMessageRepository messageRepository, IConnectionMultiplexer redis)
    : ControllerBase
{
    private readonly IDatabase _redisDb = redis.GetDatabase();

    [HttpGet("{group}")]
    public async Task<IActionResult> GetMessages(string group)
    {
        var messages = await messageRepository.GetMessagesAsync(group);
        return Ok(messages);
    }
    
    [HttpGet("set")]
    public async Task<IActionResult> SetTestValue()
    {
        await _redisDb.StringSetAsync("testkey", "Hello Redis from .NET Core");
        return Ok("Value set in Redis");
    }

    [HttpGet("get")]
    public async Task<IActionResult> GetTestValue()
    {
        var value = await _redisDb.StringGetAsync("testkey");
        return Ok(value.ToString());
    }
}