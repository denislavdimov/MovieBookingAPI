using MovieBooking.Models.Models;
using MovieBooking.Models.Requests.MovieRequests;

namespace MovieBooking.Models.Requests.BookingRequests
{
	public class BookMovieTicketRequest
	{
        public GetByIdMovieRequest Movie { get; set; }
		public string FullName { get; set; }
		public string PhoneNumber { get; set; }
		public int Seats { get; set; }
		public string Showtime { get; set; }
		public decimal Price { get; set; }
	}
}
