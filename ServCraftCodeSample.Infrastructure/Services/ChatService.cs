using Microsoft.EntityFrameworkCore;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using ServCraftCodeSample.Core.Entities;
using ServCraftCodeSample.Core.Services;
using ServCraftCodeSample.Infrastructure.Data;

namespace ServCraftCodeSample.Infrastructure.Services
{
    public class ChatService : IChatService
    {
        private readonly ChatDbContext _context;
        private readonly Kernel _kernel;

        public ChatService(ChatDbContext context, Kernel kernel)
        {
            _context = context;
            _kernel = kernel;
        }

        public async Task<Conversation> GetConversation(Guid conversationId)
        {
            var conversation = await _context.Conversations
                .Include(c => c.Messages)
                .FirstOrDefaultAsync(c => c.Id == conversationId);
            if (conversation == null)
            {
                throw new Exception("Conversation not found");
            }

            return conversation;
        }

        public async Task<ChatMessage> SendMessage(Guid conversationId, string messageText)
        {
            var conversation = await GetConversation(conversationId);
            if (conversation == null)
            {
                throw new Exception("Conversation not found");
            }

            var chatMessage = new ChatMessage
            {
                ConversationId = conversationId,
                MessageText = messageText,
                IsBot = false,
                Timestamp = DateTimeOffset.UtcNow
            };

            conversation.Messages.Add(chatMessage);

            await _context.SaveChangesAsync();

            var chatCompletionService = _kernel.GetRequiredService<IChatCompletionService>();
            var systemInstruction = "You are a chatbot that is part of a website demoing the capabilities of Dovi Joel, a coder looking to get the job of a tech lead. Start the conversation by introducing yourself and asking the other person for their name.";
            ChatHistory chatHistory = new(systemInstruction);
            chatHistory.AddRange([.. conversation.Messages
                .Select(m => new ChatMessageContent
                {
                    Content = m.MessageText,
                    Role = m.IsBot ? AuthorRole.Assistant : AuthorRole.User
                })]);

            var response = await chatCompletionService.GetChatMessageContentAsync(
                chatHistory,
                kernel: _kernel);
            
            if (response.Content == null)
            {
                throw new Exception("Failed to get response from chat completion service");
            }

            var responseMessage = new ChatMessage
            {
                ConversationId = conversationId,
                MessageText = response.Content,
                IsBot = true,
                Timestamp = DateTimeOffset.UtcNow
            };

            conversation.Messages.Add(responseMessage);

            await _context.SaveChangesAsync();

            return responseMessage;
        }

        public async Task<Conversation> StartConversation()
        {
            var conversation = new Conversation();
            _context.Conversations.Add(conversation);
            await _context.SaveChangesAsync();
            return conversation;
        }
    }
}
