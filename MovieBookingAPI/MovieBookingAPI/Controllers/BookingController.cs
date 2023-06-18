using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieBooking.BL.Interfaces;
using MovieBooking.BL.Services;
using MovieBooking.Models.Models;
using MovieBooking.Models.Requests.BookingRequests;

namespace MovieBookingAPI.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class BookingController : ControllerBase
	{
		private readonly IBookingService _bookingService;

		public BookingController(IBookingService bookingService)
		{
			_bookingService = bookingService;
		}

		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Booking))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		//[Authorize]
		[HttpPost("BookTicket")]
		public async Task<IActionResult> BookTicket([FromBody] BookMovieTicketRequest ticketRequest)
		{
			if (ticketRequest == null) return BadRequest(ticketRequest);

			await _bookingService.BookTicket(ticketRequest);

			return Ok(ticketRequest);
		}

		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Booking))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[Authorize]
		[HttpDelete("CancelBooking")]
		public async Task<IActionResult> CancelBooking([FromBody] CancelBookingRequest cancelRequest)
		{
			if (cancelRequest == null) return BadRequest(cancelRequest);

			await _bookingService.CancelBooking(cancelRequest);

			return Ok(cancelRequest);
		}

		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Booking))]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[Authorize]
		[HttpGet("GetAllTickets")]
		public async Task<IActionResult> GetAllTickets()
		{
			var result = await _bookingService.GetAllTickets();

			if (result != null && result.Any()) return Ok(result);

			return NotFound(result);
		}
	}
}
