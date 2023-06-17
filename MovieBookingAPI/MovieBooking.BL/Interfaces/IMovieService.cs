using MovieBooking.Models.Models;
using MovieBooking.Models.Requests.MovieRequests;

namespace MovieBooking.BL.Interfaces
{
	public interface IMovieService
	{
		public Task Add(AddMovieRequest addRequest);
		public Task Delete(DeleteMovieRequest deleteRequest);
		public Task<IEnumerable<Movie>> GetAll();
		public Task<Movie> GetById(GetByIdMovieRequest getByIdMovieRequest);
	}
}
