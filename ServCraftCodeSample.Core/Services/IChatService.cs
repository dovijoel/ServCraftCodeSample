using ServCraftCodeSample.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServCraftCodeSample.Core.Services;

public interface IChatService
{
    Task<Conversation> StartConversation();
    Task<Conversation> GetConversation(Guid conversationId);
    Task<ChatMessage> SendMessage(Guid conversationId, string messageText);
}
