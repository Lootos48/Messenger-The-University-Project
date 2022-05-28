using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MessengerServer.DAL.Entities
{
    public class Chat : BaseEntity
    {
        [Required]
        [StringLength(100, ErrorMessage = "Max title length is 100 characters")]
        public string Title { get; set; }

        // nav property
        public IEnumerable<Message> Messages { get; set; } = new List<Message>();
        public IEnumerable<ChatsUsers> Users { get; set; } = new List<ChatsUsers>();
    }
}
