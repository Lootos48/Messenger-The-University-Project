using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MessengerServer.DAL.Entities
{
    public class Chat
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Max title length is 100 characters")]
        public string Title { get; set; }

        // nav property
        public IEnumerable<Message> Messages { get; set; }
        public IEnumerable<ChatsUsers> Users { get; set; }
    }
}
