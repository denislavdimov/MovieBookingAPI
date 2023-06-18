using AutoMapper;
using Microsoft.Extensions.Logging;
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
		private readonly ILogger<BookingService> _logger;
		private readonly IMapper _mapper;
		private int takenSeats;

		public BookingService(IBookingRepository bookingRepository, IMapper mapper,
			IMovieService movieService, ILogger<BookingService> logger)
		{
			_bookingRepository = bookingRepository;
			_mapper = mapper;
			_movieService = movieService;
			_logger = logger;
		}

		public async Task BookTicket(BookMovieTicketRequest ticketRequest)
		{
			var ticket = _mapper.Map<Booking>(ticketRequest);

			ticket.DateAndTime = DateTime.UtcNow;
			ticket.Id = Guid.NewGuid();

			var movie = await _movieService.GetById(ticketRequest.Movie);
			var ticketPrice = movie.Price * ticketRequest.Seats;

			var allTickets = await GetAllTickets();

			foreach (var ticketSeat in allTickets)
			{
				takenSeats += ticketSeat.Seats;
			}
			ticket.TotalSeats = 20 - takenSeats;

			var validToBook = ticketRequest.Movie.Id == movie.Id && ticketRequest.Price >= ticketPrice && ticketRequest.Seats <= ticket.TotalSeats;

			if (validToBook)
			{
				ticket.TotalSeats -= ticketRequest.Seats;
				await _bookingRepository.BookTicket(ticket);
			}
			else
			{
				_logger.LogError($"{ticketRequest.Movie.Id} == {movie.Id} && {ticketRequest.Price} >= {ticketPrice} && {ticketRequest.Seats} <= {ticket.TotalSeats}");
				throw new Exception("The ticket is not added");
			}
		}

		public async Task CancelBooking(CancelBookingRequest ticketId)
		{
			var ticketCancel = _mapper.Map<Booking>(ticketId);

			await _bookingRepository.CancelBooking(ticketCancel.Id);
		}

		public async Task<IEnumerable<Booking>> GetAllTickets()
		{
			return await _bookingRepository.GetAllTickets();
		}
	}
}
