using Azure;
using Microsoft.AspNetCore.SignalR;
using ServCraftCodeSample.Core.Entities;
using ServCraftCodeSample.Core.Services;

namespace ServCraftCodeSample.Api.Hubs
{
    public class ChatHub: Hub
    {
        public readonly IChatService _chatService;
        public ChatHub(IChatService chatService)
        {
            _chatService = chatService;
        }

        public async Task StartConversation()
        {
            Console.WriteLine("Starting conversation");
            var conversation = await _chatService.StartConversation();
            await Clients.Caller.SendAsync("setConversationId", conversation.Id);
            var response = await _chatService.SendMessage(conversation.Id, "Hello");
            await Clients.Caller.SendAsync("receiveMessage", response.MessageText);
        }

        public async Task ReceiveMessage(Guid conversationId, string message)
        {
            var response = await _chatService.SendMessage(conversationId, message);
            await Clients.Caller.SendAsync("receiveMessage", response.MessageText);
        }
    }
}
