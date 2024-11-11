using RealtimeChatApp.RealtimeChatApp.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealtimeChatApp.RealtimeChatApp.Domain.Entities
{
    public class Challenges
    {
        public int Id { get; set; }
        public int InitiatorId { get; set; }
        public int ReceiverId { get; set; }

        [Column("challange_text")]
        public string ChallangeText { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        [Column("status")]
        public ChallengeStatus Status { get; set; } // Use enum for status
        [Column("message_id")]
        public int? MessageId { get; set; } // Nullable to handle scenarios where there isn't an associated message yet
        [Column("penalty")]
        public string Penalty { get; set; } // Penalty for failing the challenge (e.g., a playful punishment)
    }
}
