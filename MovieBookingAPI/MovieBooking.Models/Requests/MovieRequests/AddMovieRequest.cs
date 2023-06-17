namespace MovieBooking.Models.Requests.MovieRequests
{
	public class AddMovieRequest
	{
		public string Name { get; set; }
		public decimal Price { get; set; }
		public string Showtime { get; set; }
	}
}
