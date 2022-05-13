using System;

namespace MessengerServer.DAL.Entities
{
    public class Message : BaseEntity
    {
        public DateTime SendTime { get; set; }
        public string Text { get; set; }

        // nav property
        public int? ImageId { get; set; }
        public Image Picture { get; set; }

        public int UserId { get; set; }
        public User Sender { get; set; }

        public int ChatId { get; set; }
        public Chat Chat { get; set; }
    }
}