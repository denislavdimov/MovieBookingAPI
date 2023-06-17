using MovieBooking.Models.Models;

namespace MovieBooking.DL.Interfaces
{
	public interface IBookingRepository
	{
		public Task BookTicket(Booking ticket);
		public Task CancelBooking(Booking ticketId);
	}
}
