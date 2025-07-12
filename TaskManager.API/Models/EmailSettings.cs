namespace TaskManager.API.Models
{
    public class EmailSettings
    {
        public required string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public required string SenderEmail { get; set; }
        public required string SenderName { get; set; }
        public required string Password { get; set; }
    }
}
