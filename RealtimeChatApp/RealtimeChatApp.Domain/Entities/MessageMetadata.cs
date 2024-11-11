using System.ComponentModel.DataAnnotations.Schema;

namespace RealtimeChatApp.RealtimeChatApp.Domain.Entities
{
    public class MessageMetadata
    {
        public int Id { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        [Column("message_id")]
        public int MessageId { get; set; }
        [Column("media_url")]
        public string MediaUrl { get; set; }
        [Column("media_type")]
        public string MediaType { get; set; }
    }
}
