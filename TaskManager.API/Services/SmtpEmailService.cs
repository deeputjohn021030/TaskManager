using MongoDB.Driver;
using TaskManager.API.Models;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace TaskManager.API.Services
{
    public class SmtpEmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoCollection<TaskItem> _tasks;

        public SmtpEmailService(IConfiguration configuration, IMongoDatabase database)
        {
            _configuration = configuration;
            _tasks = database.GetCollection<TaskItem>("Tasks");
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            var smtpHost = _configuration["Email:SmtpHost"];
            var smtpPort = int.Parse(_configuration["Email:SmtpPort"]);
            var smtpUser = _configuration["Email:SmtpUser"];
            var smtpPass = _configuration["Email:SmtpPass"];
            var fromEmail = _configuration["Email:FromEmail"];

            var client = new SmtpClient(smtpHost, smtpPort)
            {
                Credentials = new NetworkCredential(smtpUser, smtpPass),
                EnableSsl = true
            };

            var mailMessage = new MailMessage(fromEmail, toEmail, subject, message);
            await client.SendMailAsync(mailMessage);
        }

        // ✅ MongoDB-compatible filter for tasks due within next 30 minutes
        public async Task<List<TaskItem>> GetTasksDueSoonAsync()
        {
            var now = DateTime.Now;
            var in30Min = now.AddMinutes(30);

            var filter = Builders<TaskItem>.Filter.And(
                Builders<TaskItem>.Filter.Eq(t => t.IsCompleted, false),
                Builders<TaskItem>.Filter.Gte(t => t.DueDate, now),
                Builders<TaskItem>.Filter.Lte(t => t.DueDate, in30Min)
            );

            return await _tasks.Find(filter).ToListAsync();
        }
    }
}
