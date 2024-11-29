using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;

namespace RealtimeChatApp.RealtimeChatApp.API.Hubs
{
    public class ChatHub : Hub
    {
        // This method will be called when a user sends a message
        public async Task SendMessage(string chatId, string userId, string message)
        {
            await Clients.Group(chatId).SendAsync("ReceiveMessage", userId, message);
        }

        // This method can be used to add a user to a specific chat group
        public async Task JoinGroup(string chatId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
        }

        // This method will be called when a user leaves a group
        public async Task LeaveGroup(string chatId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId);
        }
    }
}
