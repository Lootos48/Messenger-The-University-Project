using System.Collections.Generic;

namespace MessengerServer.DTOs
{
    public class ChatDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public List<UserDTO> Users { get; set; } = new List<UserDTO>();
        public List<MessageDTO> Messages { get; set; } = new List<MessageDTO>();
    }
}
