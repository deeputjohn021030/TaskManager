using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Security.Claims;
using TaskManager.API.Models;
using TaskManager.API.Services;
using TaskManager.API.DTOs;

namespace TaskManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly MongoDbService _mongoService;
        private readonly IEmailService _emailService;
       
        public TasksController(MongoDbService mongoService, IEmailService emailService)
        {
            _mongoService = mongoService;
            _emailService = emailService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID not found in token.");
            }

            var tasks = await _mongoService.GetTasksByUserIdAsync(userId);
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            if (!ObjectId.TryParse(id, out _))
                return BadRequest("Invalid ID format");

            var task = await _mongoService.GetAsync(id);
            return task == null ? NotFound() : Ok(task);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TaskItem task)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userEmail = User.FindFirstValue(ClaimTypes.Email);        

            task.UserId = userId;
            task.UserEmail = userEmail;

            task.Id = null;
            task.CreatedAt = DateTime.UtcNow;

            await _mongoService.CreateAsync(task);
            return CreatedAtAction(nameof(Get), new { id = task.Id }, task);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] TaskItem taskIn)
        {
            if (!ObjectId.TryParse(id, out _))
                return BadRequest("Invalid ID format");

            var existing = await _mongoService.GetAsync(id);
            if (existing == null)
                return NotFound();

            taskIn.Id = id;
            await _mongoService.UpdateAsync(id, taskIn);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (!ObjectId.TryParse(id, out _))
                return BadRequest("Invalid ID format");

            var task = await _mongoService.GetAsync(id);
            if (task == null) return NotFound();

            await _mongoService.RemoveAsync(id);
            return NoContent();
        }


        [HttpPost("send-reminder")]
        public async Task<IActionResult> SendReminder([FromQuery] string email, [FromQuery] string taskTitle)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(taskTitle))
                return BadRequest("Email and task title are required.");

            await _emailService.SendEmailAsync(email, "Task Reminder", $"Don't forget: {taskTitle}");
            return Ok("Email sent.");
        }
    }
}
