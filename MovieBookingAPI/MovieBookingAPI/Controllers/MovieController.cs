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
		//private readonly IConfiguration _configuration;

		public MovieController(IMovieService movieService, IConfiguration configuration)
		{
			_movieService = movieService;
			//_configuration = configuration;
		}

		[HttpPost("Add")]
		public async Task Add(AddMovieRequest addMovieRequest)
		{
			await _movieService.Add(addMovieRequest);
		}

		[HttpDelete("Delete")]
		public async Task Delete(DeleteMovieRequest deleteMovieRequest)
		{
			await _movieService.Delete(deleteMovieRequest);
		}

		[HttpGet("GetAll")]
		public async Task<IEnumerable<Movie>> GetAll()
		{
			return await _movieService.GetAll();
		}

		[HttpGet("GetById")]
		public async Task<Movie> GetById([FromQuery] GetByIdMovieRequest getByIdMovieRequest)
		{
			return await _movieService.GetById(getByIdMovieRequest);
		}
	}
}
