using TaskManager.API.Models;
using TaskManager.API.Services;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskManager.API.Services
{
    public class TaskService : ITaskService
    {
        private readonly IMongoCollection<TaskItem> _taskCollection;
        private readonly IEmailService _emailService;

        public TaskService(IMongoDatabase database, IEmailService emailService)
        {
            _taskCollection = database.GetCollection<TaskItem>("Tasks");
            _emailService = emailService;
        }

        public async Task<List<TaskItem>> GetPendingTasksWithRemindersAsync()
        {
            return await _taskCollection
                .Find(t => !t.IsCompleted && t.ReminderTime != "")
                .ToListAsync();
        }

        public async Task SendTaskRemindersAsync()
        {
            var tasks = await GetPendingTasksWithRemindersAsync();
            var now = DateTime.UtcNow;

            foreach (var task in tasks)
            {
                if (TimeSpan.TryParse(task.ReminderTime, out var reminderTime))
                {
                    var reminderDateTime = new DateTime(
                        now.Year, now.Month, now.Day,
                        reminderTime.Hours, reminderTime.Minutes, 0, DateTimeKind.Utc);

                    if (Math.Abs((now - reminderDateTime).TotalMinutes) <= 1)
                    {
                        await _emailService.SendEmailAsync(
                            task.UserEmail,
                            "Task Reminder",
                            $"⏰ Reminder: Your task \"{task.Title}\" is due at {task.DueDate.ToLocalTime():f}."
                        );
                    }
                }
            }
        }
    }
}
