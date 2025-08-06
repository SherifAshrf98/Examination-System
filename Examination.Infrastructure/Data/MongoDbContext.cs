using Examination.Application.Common;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Infrastructure.Data
{
	public class MongoDbContext
	{
		private readonly IMongoDatabase _database;
		public MongoDbContext(IMongoClient mongoClient, IOptions<MongoDbSettings> settings)
		{
			_database = mongoClient.GetDatabase(settings.Value.DatabaseName);
		}
		public IMongoCollection<NotificationMessage> Notifications => _database.GetCollection<NotificationMessage>("Notifications");
	}
}
