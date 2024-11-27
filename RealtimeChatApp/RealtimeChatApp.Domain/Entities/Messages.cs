using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealtimeChatApp.RealtimeChatApp.Domain.Entities
{
    public class Messages
    {
        public int Id { get; set; }

        [Column("sender_id")]
        public int SenderId { get; set; }

        [Column("recevier_id")]
        public int ReceiverId { get; set; }

        [Column("content")]
        public string Content { get; set; }

        [Column("timestamp")]
        public DateTime Timestamp { get; set; }

        [Column("read")]
        public bool IsRead { get; set; }

        [Column("is_challange")]
        public bool IsChallenge { get; set; }

        // Foreign key property for the Chats table
        [Column("ChatId")]
        public Guid ChatId { get; set; }

        // Navigation property for the parent chat
        public Chats Chat { get; set; }
    }
}
