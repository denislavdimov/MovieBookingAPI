using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieBooking.BL.Interfaces;
using MovieBooking.Models.Models;
using MovieBooking.Models.Requests.MovieRequests;

namespace MovieBookingAPI.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class MovieController : ControllerBase
	{
		private readonly IMovieService _movieService;

		public MovieController(IMovieService movieService)
		{
			_movieService = movieService;
		}

		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Movie))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[Authorize]
		[HttpPost("Add")]
		public async Task<IActionResult> Add([FromBody] AddMovieRequest addMovieRequest)
		{
			if (addMovieRequest == null) return BadRequest(addMovieRequest);

			await _movieService.Add(addMovieRequest);

			return Ok(addMovieRequest);
		}

		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Movie))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[Authorize]
		[HttpDelete("Delete")]
		public async Task<IActionResult> Delete(DeleteMovieRequest deleteMovieRequest)
		{
			if (deleteMovieRequest == null) return BadRequest(deleteMovieRequest);

			await _movieService.Delete(deleteMovieRequest);

			return Ok(deleteMovieRequest);
		}

		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Movie>))]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[Authorize]
		[HttpGet("GetAll")]
		public async Task<IActionResult> GetAll()
		{
			var result = await _movieService.GetAll();

			if (result != null && result.Any()) return Ok(result);

			return NotFound(result);
		}

		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Movie))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[Authorize]
		[HttpGet("GetById")]
		public async Task<IActionResult> GetById([FromQuery] GetByIdMovieRequest getByIdMovieRequest)
		{
			if (getByIdMovieRequest == null) return BadRequest(getByIdMovieRequest);

			var result = await _movieService.GetById(getByIdMovieRequest);

			if (result != null) return Ok(result);

			return NotFound(getByIdMovieRequest);
		}
	}
}
