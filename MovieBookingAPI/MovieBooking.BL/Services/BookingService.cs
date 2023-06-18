using AutoMapper;
using MovieBooking.BL.Interfaces;
using MovieBooking.DL.Interfaces;
using MovieBooking.Models.Models;
using MovieBooking.Models.Requests.BookingRequests;

namespace MovieBooking.BL.Services
{
	public class BookingService : IBookingService
	{
		private readonly IBookingRepository _bookingRepository;
		private readonly IMovieService _movieService;
		private readonly IMapper _mapper;

		public BookingService(IBookingRepository bookingRepository, IMapper mapper,
			IMovieService movieService)
		{
			_bookingRepository = bookingRepository;
			_mapper = mapper;
			_movieService = movieService;
		}

		public async Task BookTicket(BookMovieTicketRequest ticketRequest)
		{
			var ticket = _mapper.Map<Booking>(ticketRequest);

			ticket.DateAndTime = DateTime.UtcNow;
			ticket.Id = Guid.NewGuid();
			
			var movie = await _movieService.GetById(ticketRequest.Movie);
			var ticketPrice = movie.Price * ticketRequest.Seats;

			var validToBook = ticketRequest.Movie.Id == movie.Id && ticketRequest.Price >= ticketPrice && ticketRequest.Seats <= ticket.TotalSeats;

			if (validToBook)
			{
				await _bookingRepository.BookTicket(ticket);
				ticket.TotalSeats -= ticketRequest.Seats;
			}
		}

		public async Task CancelBooking(CancelBookingRequest ticketId)
		{
			var ticketCancel = _mapper.Map<Booking>(ticketId);

			await _bookingRepository.CancelBooking(ticketCancel.Id);
		}
	}
}
