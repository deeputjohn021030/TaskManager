using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace TaskManager.API.Models
{
    public class TaskItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("title")]
        public string Title { get; set; } = "";

        [BsonElement("isCompleted")]
        public bool IsCompleted { get; set; }

        [BsonElement("CreatedAt")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [BsonElement("UserId")]
        public string UserId { get; set; } = string.Empty;

        [BsonElement("DueDate")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime DueDate { get; set; }

        [BsonElement("UserEmail")]
        public string UserEmail { get; set; } = string.Empty;

        [BsonElement("ReminderTime")]
        public string ReminderTime { get; set; } = string.Empty; // format: "HH:mm"
    }
}
