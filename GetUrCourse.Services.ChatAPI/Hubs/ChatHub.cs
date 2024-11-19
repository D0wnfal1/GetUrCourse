using GetUrCourse.Services.ChatAPI.Models;
using GetUrCourse.Services.ChatAPI.Repositories;
using Microsoft.AspNetCore.SignalR;

namespace GetUrCourse.Services.ChatAPI.Hubs;

public class ChatHub : Hub
{
    private readonly IMessageRepository _messageRepository;

    public ChatHub(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task SendMessage(string user, string group, string message)
    {
        var chatMessage = new Message
        {
            User = user,
            Group = group,
            Text = message,
            Timestamp = DateTime.UtcNow
        };

        await _messageRepository.SaveMessageAsync(chatMessage);
        await Clients.Group(group).SendAsync("ReceiveMessage", user, message);
    }

    public async Task JoinGroup(string group)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, group);
        var messages = await _messageRepository.GetMessagesAsync(group);
        await Clients.Caller.SendAsync("ReceiveMessageHistory", messages);
    }

    public async Task LeaveGroup(string group)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, group);
    }
}