using Amazon.Runtime.Internal.Util;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using MovieBooking.BL.Interfaces;
using MovieBooking.BL.Services;
using MovieBooking.DL.Interfaces;
using MovieBooking.Models.Models;
using MovieBooking.Models.Requests.BookingRequests;
using MovieBooking.Models.Requests.MovieRequests;
using MovieBookingAPI.AutoMapper;

namespace MovieBooking.Test.BookingServiceTests
{
	public class BookingServiceTest
	{
		private readonly Mock<IBookingRepository> _bookingRepository;
		private readonly Mock<IMovieService> _movieService;
		private readonly BookingService _bookingService;
		private readonly IMapper _mapper;
		private readonly Mock<ILogger<BookingService>> _logger;

		private IList<Booking> Bookings = new List<Booking>()
		{
			new Booking()
			{
				Id = new Guid("0c18f6ae-a16c-477e-b267-9184c5420836"),
				FullName = "Denislav Dimov",
				PhoneNumber = "1234567890",
				TotalSeats = 25,
				Seats = 2,
				DateAndTime = DateTime.UtcNow,
				Showtime = "21:00",
				Price = 15
			},

			new Booking()
			{
				Id = new Guid("0c18f6ae-a16c-477e-b267-9184c2278340"),
				FullName = "Test Dimov",
				PhoneNumber = "888553289",
				TotalSeats = 25,
				Seats = 3,
				DateAndTime = DateTime.UtcNow,
				Showtime = "19:30",
				Price = 18
			}
		};

		public BookingServiceTest()
		{
			_bookingRepository = new Mock<IBookingRepository>();
			_movieService = new Mock<IMovieService>();

			var mockMapper = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile(new AutoMapperProfile());
			});
			var mapper = mockMapper.CreateMapper();
			_mapper = mapper;

			_logger = new Mock<ILogger<BookingService>>();	

			_bookingService = new BookingService(_bookingRepository.Object, _mapper, _movieService.Object, _logger.Object);
		}

		[Fact]
		public async Task Booking_BookMovieTicket_Test()
		{
			var bookingTicket = new Booking()
			{
				Id = new Guid("0c18f6ae-a16c-477e-b267-6543c2278000"),
				FullName = "Dimov Booking",
				PhoneNumber = "1111111119",
				TotalSeats = 25,
				Seats = 5,
				DateAndTime = DateTime.UtcNow,
				Showtime = "19:30",
				Price = 200
			};

			var bookingTicketRequest = new BookMovieTicketRequest()
			{
				Movie = new GetByIdMovieRequest()
				{
					Id = new Guid("0c18f6ae-a16c-477e-b267-9184c4819742")
				},
				FullName = "Dimov Booking",
				PhoneNumber = "1111111119",
				Seats = 5,
				Showtime = "19:30",
				Price = 200
			};

			var movie = new Movie()
			{
				Id = new Guid("0c18f6ae-a16c-477e-b267-9184c4819742"),
				Name = "John Wick IV",
				Price = 15,
				Showtime = "19:30"
			};

			_movieService.Setup(a => a.GetById(bookingTicketRequest.Movie))
				.Returns(() => Task.FromResult(movie));

			_bookingRepository.Setup(b => b.BookTicket(It.IsAny<Booking>())).Callback(() =>	
			Bookings.Add(bookingTicket)).Returns(Task.CompletedTask);

			await _bookingService.BookTicket(bookingTicketRequest);

			Bookings.Should().HaveCount(3);
			Bookings.Should().Contain(bookingTicket);
		}

		[Fact]
		public async Task Booking_CancelBooking_Test()
		{
			var bookingId = new Guid("0c18f6ae-a16c-477e-b267-9184c2278340");

			var booking = new CancelBookingRequest()
			{
				Id= bookingId
			};

			var bookingToCancel = Bookings.First(m => m.Id == bookingId);

			_bookingRepository.Setup(d => d.CancelBooking(booking.Id)).Returns(async () => Bookings.Remove(bookingToCancel));

			await _bookingService.CancelBooking(booking);

			Bookings.Should().HaveCount(1);
			Bookings.Should().NotContain(bookingToCancel);
		}
	}
}
