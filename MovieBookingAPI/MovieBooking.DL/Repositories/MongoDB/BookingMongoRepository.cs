using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MovieBooking.DL.Interfaces;
using MovieBooking.Models.Configuration;
using MovieBooking.Models.Models;

namespace MovieBooking.DL.Repositories.MongoDB
{
	public class BookingMongoRepository : IBookingRepository
	{
		private readonly IMongoCollection<Booking> _booking;

		public BookingMongoRepository(IOptionsMonitor<MongoDbConfiguration> mongoConfig)
		{
			var client = new MongoClient(mongoConfig.CurrentValue.ConnectionString);
			var database = client.GetDatabase(mongoConfig.CurrentValue.DatabaseName);
			var collectionSettings = new MongoCollectionSettings
			{
				GuidRepresentation = GuidRepresentation.Standard
			};
			_booking = database.GetCollection<Booking>(nameof(Booking), collectionSettings);
		}

		public async Task BookTicket(Booking ticket)
		{
			await _booking.InsertOneAsync(ticket);
		}

		public async Task CancelBooking(Guid ticketId)
		{
			await _booking.DeleteOneAsync(t => t.Id == ticketId);
		}
	}
}
