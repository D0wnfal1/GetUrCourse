using GetUrCourse.Services.ChatAPI.Models;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace GetUrCourse.Services.ChatAPI.Repositories;

public class RedisMessageRepository(IConnectionMultiplexer redis) : IMessageRepository
{
    private readonly IDatabase _database = redis.GetDatabase();

    public async Task SaveMessageAsync(Message message)
    {
        var serializedMessage = JsonConvert.SerializeObject(message);
        await _database.ListRightPushAsync(message.Group, serializedMessage);
    }

    public async Task<IEnumerable<Message>> GetMessagesAsync(string group)
    {
        var messages = await _database.ListRangeAsync(group);
        var result = new List<Message>();

        foreach (var message in messages)
        {
            result.Add(JsonConvert.DeserializeObject<Message>(message));
        }

        return result;
    }
}