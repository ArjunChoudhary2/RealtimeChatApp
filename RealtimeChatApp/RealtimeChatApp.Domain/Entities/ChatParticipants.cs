using System.ComponentModel.DataAnnotations.Schema;
namespace RealtimeChatApp.RealtimeChatApp.Domain.Entities
{
    public class ChatParticipants
    {
        public int Id { get; set; }
        [Column("chat_id")]
        public int ChatId { get; set; }
        [Column("joined_at")]
        public DateTime JoinedAt { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
    }

}
