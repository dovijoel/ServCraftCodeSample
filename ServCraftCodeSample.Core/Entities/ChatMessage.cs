using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServCraftCodeSample.Core.Entities
{
    public class ChatMessage: BaseEntity
    {
        public Conversation? Conversation { get; set; }
        public Guid ConversationId { get; set; }
        public required string MessageText { get; set; }
        public required bool IsBot { get; set; }
        public required DateTimeOffset Timestamp { get; set; }

    }
}
