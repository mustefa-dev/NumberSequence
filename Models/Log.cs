namespace Auth.Models
{
    public class Log
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
    }
}