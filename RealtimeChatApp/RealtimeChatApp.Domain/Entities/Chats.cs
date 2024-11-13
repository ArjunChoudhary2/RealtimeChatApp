using System.ComponentModel.DataAnnotations.Schema;
namespace RealtimeChatApp.RealtimeChatApp.Domain.Entities
{
    public class Chats
    {

        public int Id { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        [Column("user1_id")]
        public int User1_Id { get; set; }
        [Column("user1_id")]
        public int User2_Id { get; set; }
        [Column("chat_name")]
        public string? ChatName { get; set; }
    }
}
