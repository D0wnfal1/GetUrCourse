using GetUrCourse.Services.ChatAPI.Models;

namespace GetUrCourse.Services.ChatAPI.Repositories;

public interface IMessageRepository
{
    Task SaveMessageAsync(Message message);
    Task<IEnumerable<Message>> GetMessagesAsync(string group);
}