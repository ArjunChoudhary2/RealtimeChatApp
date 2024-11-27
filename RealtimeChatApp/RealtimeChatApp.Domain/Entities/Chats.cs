using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace RealtimeChatApp.RealtimeChatApp.Domain.Entities
{
    public class Chats
    {
        [Key]
        public Guid Id { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        [Column("user1_id")]
        public Guid User1_Id { get; set; }
        [Column("user2_id")]
        public Guid User2_Id { get; set; }
        [Column("chat_name")]
        public string? ChatName { get; set; }

        // Navigation property for related messages
        public ICollection<Messages> Messages { get; set; }
    }
}
