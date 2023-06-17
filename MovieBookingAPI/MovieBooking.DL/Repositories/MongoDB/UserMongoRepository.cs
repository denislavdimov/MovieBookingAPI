using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MovieBooking.DL.Interfaces;
using MovieBooking.Models.Configuration;
using MovieBooking.Models.Models;

namespace MovieBooking.DL.Repositories.MongoDB
{
	public class UserMongoRepository : IUserRepository
	{
		private readonly IMongoCollection<User> _user;

		public UserMongoRepository(IOptionsMonitor<MongoDbConfiguration> mongoConfig)
		{
			var client = new MongoClient(mongoConfig.CurrentValue.ConnectionString);
			var database = client.GetDatabase(mongoConfig.CurrentValue.DatabaseName);
			var collectionSettings = new MongoCollectionSettings
			{
				GuidRepresentation = GuidRepresentation.Standard
			};
			_user = database.GetCollection<User>(nameof(User), collectionSettings);
		}

		public async Task Add(User user)
		{
			await _user.InsertOneAsync(user);
		}

		public async Task<User?> GetUserInfo(string email, string password)
		{
			return await _user.Find(u => u.Email == email && u.Password == password).FirstOrDefaultAsync();
		}
	}
}
