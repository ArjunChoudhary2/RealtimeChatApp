using System.ComponentModel.DataAnnotations.Schema;

namespace RealtimeChatApp.RealtimeChatApp.Domain.Entities
{
    public class Notifications
    {
        public int Id { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("message")]
        public string Message { get; set; }
        [Column("status")]
        public string Status { get; set; }
    }
}
