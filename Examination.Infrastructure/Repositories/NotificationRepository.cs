using Examination.Application.Interfaces.Repositories;
using Examination.Infrastructure.Data;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Infrastructure.Repositories
{
	public class NotificationRepository : INotificationRepository
	{
		private readonly MongoDbContext _mongoDb;

		public NotificationRepository(MongoDbContext mongoDb)
		{
			_mongoDb = mongoDb;
		}
		public async Task AddNotificationAsync(NotificationMessage notification)
		{
			await _mongoDb.Notifications.InsertOneAsync(notification);
		}

		public async Task<List<NotificationMessage>> GetNotificationsByUserIdAsync(string userId)
		{
			return await _mongoDb.Notifications.Find(n => n.UserId == userId && n.UserRole != "Admin")
				.SortByDescending(n => n.Timestamp)
				.ToListAsync();
		}

		public async Task<List<NotificationMessage>> GetNotificationsByUserRoleAsync(string userRole)
		{
			return await _mongoDb.Notifications
				.Find(n => n.UserRole == userRole && n.UserId == null)
				.SortByDescending(n => n.Timestamp)
				.ToListAsync();
		}

		public async Task<bool> MarkAsReadAsync(string notificationId)
		{
			var result = await _mongoDb.Notifications.UpdateOneAsync(
				n => n.Id == notificationId,
				Builders<NotificationMessage>.Update.Set(n => n.IsRead, true)
			);

			return result.ModifiedCount > 0;
		}
	}
}
