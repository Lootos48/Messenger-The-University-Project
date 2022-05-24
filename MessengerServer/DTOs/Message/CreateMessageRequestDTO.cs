namespace MessengerServer.DTOs.Message
{
    public class CreateMessageRequestDTO
    {
        public string Text { get; set; }

        public byte?[] ImageBytes { get; set; }

        public int UserId { get; set; }

        public int ChatId { get; set; }
    }
}
