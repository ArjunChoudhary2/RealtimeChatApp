using System.ComponentModel.DataAnnotations.Schema;

namespace RealtimeChatApp.RealtimeChatApp.Domain.Entities
{
    public class UserStatusHistory
    {
        public int Id { get; set; }
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
        [Column("mood_status")]
        public string MoodStatus { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
    }
}
