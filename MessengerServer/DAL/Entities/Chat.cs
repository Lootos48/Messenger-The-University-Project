using System.Collections;
using System.Collections.Generic;

namespace MessengerServer.DAL.Entities
{
    public class Chat
    {
        public int Id { get; set; }
        public string Title { get; set; }

        // nav property
        public IEnumerable<Message> Messages { get; set; }
        public IEnumerable<ChatsUsers> Users { get; set; }
    }
}
