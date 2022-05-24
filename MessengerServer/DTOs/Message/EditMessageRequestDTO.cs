namespace MessengerServer.DTOs.Message
{
    public class EditMessageRequestDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public byte?[] ImageBytes { get; set; }
    }
}
