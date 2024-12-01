using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealtimeChatApp.RealtimeChatApp.Domain.Entities
{
    public class Messages
    {
        public int Id { get; set; }

        [Column("sender_id")]
        public Guid SenderId { get; set; }

        [Column("recevier_id")]
        public Guid ReceiverId { get; set; }
        
        [Column("content")]
        public string Content { get; set; }

        [Column("timestamp")]
        public DateTime Timestamp { get; set; }

        [Column("read")]
        public bool IsRead { get; set; }

        [Column("is_challange")]
        public bool IsChallenge { get; set; }

        // Foreign key property for the Chats table
        [ForeignKey(nameof(Chat))]
        public Guid ChatId { get; set; }

        // Navigation property for the parent chat
        public Chats Chat { get; set; }
    }
}
