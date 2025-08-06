namespace Examination.Application.Dtos
{
	public class NotificationMessageDto
	{
		public string Id { get; set; }

		public string Message { get; set; }

		public DateTime Timestamp { get; set; }

		public bool IsRead { get; set; }

	}
}