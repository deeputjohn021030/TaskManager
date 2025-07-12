namespace TaskManager.API.DTOs
{
    public class CreateTaskDto
    {

        public string Title { get; set; } = string.Empty;

        public DateTime DueDate { get; set; }

        public string ReminderTime { get; set; } = string.Empty;
    }
}
