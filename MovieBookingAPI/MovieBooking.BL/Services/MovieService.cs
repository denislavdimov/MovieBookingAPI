using AutoMapper;
using MovieBooking.BL.Interfaces;
using MovieBooking.DL.Interfaces;
using MovieBooking.Models.Models;
using MovieBooking.Models.Requests.MovieRequests;

namespace MovieBooking.BL.Services
{
	public class MovieService : IMovieService
	{
		private readonly IMovieRepository _movieRepository;
		private readonly IMapper _mapper;

		public MovieService(IMovieRepository movieRepository, IMapper mapper)
		{
			_movieRepository = movieRepository;
			_mapper = mapper;
		}

		public async Task Add(AddMovieRequest addRequest)
		{
			var movie = _mapper.Map<Movie>(addRequest);

			movie.Id = Guid.NewGuid();

			await _movieRepository.Add(movie);
		}

		public async Task Delete(DeleteMovieRequest deleteRequest)
		{
			var movieToDelete = _mapper.Map<Movie>(deleteRequest);

			await _movieRepository.Delete(deleteRequest.Id);
		}

		public async Task<IEnumerable<Movie>> GetAll()
		{
			return await _movieRepository.GetAll();
		}

		public async Task<Movie> GetById(GetByIdMovieRequest getByIdMovieRequest)
		{
			var movieToGet = _mapper.Map<Movie>(getByIdMovieRequest);

			return await _movieRepository.GetById(movieToGet.Id);
		}
	}
}
