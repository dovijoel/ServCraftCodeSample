using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServCraftCodeSample.Core.Entities
{
    public class Conversation: BaseEntity
    {
        public List<ChatMessage> Messages { get; set; } = [];
    }
}
