using TaskManager.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskManager.API.Services
{
    public interface ITaskService
    {
        Task<List<TaskItem>> GetPendingTasksWithRemindersAsync();
        Task SendTaskRemindersAsync();
    }
}
