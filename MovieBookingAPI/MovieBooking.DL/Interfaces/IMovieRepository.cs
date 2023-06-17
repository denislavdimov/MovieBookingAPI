using MovieBooking.Models.Models;

namespace MovieBooking.DL.Interfaces
{
	public interface IMovieRepository
	{
		public Task Add(Movie movie);
		public Task Delete(Movie movieId);
		public Task<IEnumerable<Movie>> GetAll();
		public Task<Movie?> GetById(Movie movieId);
	}
}
