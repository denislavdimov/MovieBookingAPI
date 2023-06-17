namespace MovieBooking.Models.Configuration
{
	public class MongoDbConfiguration
	{
		public string ConnectionString { get; set; }

		public string DatabaseName { get; set; } = string.Empty;
	}
}
