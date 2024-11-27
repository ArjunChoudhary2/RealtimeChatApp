using System.ComponentModel.DataAnnotations.Schema;

namespace RealtimeChatApp.RealtimeChatApp.Domain.Entities
{
    [Table("users")]
    public class Users
    {
        public Guid Id { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
        [Column("username")]
        public string Username { get; set; }
        [Column("profile_pic")]
        public string? ProfilePicture { get; set; }
        [Column("mood_status")]
        public string? MoodStatus { get; set; }
    }
}
