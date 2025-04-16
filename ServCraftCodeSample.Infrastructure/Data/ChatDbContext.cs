using Microsoft.EntityFrameworkCore;
using ServCraftCodeSample.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServCraftCodeSample.Infrastructure.Data
{
    public class ChatDbContext: DbContext
    {
        public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options)
        {
        }
        public DbSet<ChatMessage> ChatMessages { get; set; } = null!;
        public DbSet<Conversation> Conversations { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChatMessage>()
                .HasOne(c => c.Conversation)
                .WithMany(c => c.Messages)
                .HasForeignKey(c => c.ConversationId);
        }
    }
}
