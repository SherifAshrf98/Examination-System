using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Examination.Infrastructure.Data
{
	public class NotificationMessage
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }
		public string? UserId { get; set; }
		public string? UserRole { get; set; }
		public string Message { get; set; }
		public DateTime Timestamp { get; set; }		
		public string? Type { get; set; }
		public bool IsRead { get; set; }
	}
}