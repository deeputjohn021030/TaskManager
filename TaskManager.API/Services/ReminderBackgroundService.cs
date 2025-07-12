using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.API.Services;

namespace TaskManager.API.BackgroundServices
{
    public class ReminderBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public ReminderBackgroundService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var taskService = scope.ServiceProvider.GetRequiredService<ITaskService>();
                    await taskService.SendTaskRemindersAsync();
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken); // run every 1 minute
            }
        }
    }
}
