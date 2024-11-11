using System.ComponentModel.DataAnnotations.Schema;

namespace RealtimeChatApp.RealtimeChatApp.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        [Column("created_at")]
        public int CreatedAt { get; set; }
        public string Username { get; set; }
        public string ProfilePicture { get; set; }
        public string MoodStatus { get; set; }
    }
}
