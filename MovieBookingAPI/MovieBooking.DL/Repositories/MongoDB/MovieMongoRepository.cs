using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MovieBooking.DL.Interfaces;
using MovieBooking.Models.Configuration;
using MovieBooking.Models.Models;

namespace MovieBooking.DL.Repositories.MongoDB
{
	public class MovieMongoRepository : IMovieRepository
	{
		private readonly IMongoCollection<Movie> _movie;

		public MovieMongoRepository(IOptionsMonitor<MongoDbConfiguration> mongoConfig)
		{
			var client = new MongoClient(mongoConfig.CurrentValue.ConnectionString);
			var database = client.GetDatabase(mongoConfig.CurrentValue.DatabaseName);
			var collectionSettings = new MongoCollectionSettings
			{
				GuidRepresentation = GuidRepresentation.Standard
			};
			_movie = database.GetCollection<Movie>(nameof(Movie), collectionSettings);
		}

		public async Task Add(Movie movie)
		{
			await _movie.InsertOneAsync(movie);
		}

		public async Task Delete(Guid movieId)
		{
			await _movie.DeleteOneAsync(m => m.Id == movieId);
		}

		public async Task<IEnumerable<Movie>> GetAll()
		{
			return await _movie.Find(m => m.Id != null).ToListAsync();
		}

		public async Task<Movie?> GetById(Guid movieId)
		{
			var movie = await _movie.Find(movie => movie.Id == movieId).FirstOrDefaultAsync();
			return movie;
		}
	}
}
