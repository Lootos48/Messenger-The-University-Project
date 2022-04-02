namespace MessengerServer.Entities
{
    public class ChatsUsers
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int ChatId { get; set; }
        public Chat Chat { get; set; }
    }
}