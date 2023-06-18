using MovieBooking.Models.Models;
using MovieBooking.Models.Requests.BookingRequests;

namespace MovieBooking.BL.Interfaces
{
	public interface IBookingService
	{
		public Task BookTicket(BookMovieTicketRequest ticketRequest);
		public Task CancelBooking(CancelBookingRequest ticketId);
		public Task<IEnumerable<Booking>> GetAllTickets();
	}
}
