using System.Collections.Concurrent;
using GetUrCourse.Services.ChatAPI.Models;

namespace GetUrCourse.Services.ChatAPI.Repositories;

public class InMemoryMessageRepository : IMessageRepository
{
    private readonly ConcurrentDictionary<string, List<Message>> _messages = new ConcurrentDictionary<string, List<Message>>();

    public Task SaveMessageAsync(Message message)
    {
        if (!_messages.ContainsKey(message.Group))
        {
            _messages[message.Group] = new List<Message>();
        }
        _messages[message.Group].Add(message);
        return Task.CompletedTask;
    }

    public Task<IEnumerable<Message>> GetMessagesAsync(string group)
    {
        _messages.TryGetValue(group, out var messages);
        return Task.FromResult<IEnumerable<Message>>(messages ?? new List<Message>());
    }
}