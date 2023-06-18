using AutoMapper;
using FluentAssertions;
using Moq;
using MovieBooking.BL.Services;
using MovieBooking.DL.Interfaces;
using MovieBooking.Models.Models;
using MovieBooking.Models.Requests.MovieRequests;
using MovieBookingAPI.AutoMapper;

namespace MovieBooking.Test.MovieServiceTests
{
	public class MovieServiceTest
	{
		private readonly Mock<IMovieRepository> _movieRepository;
		private readonly MovieService _movieService;
		private readonly Movie _movie;
		private readonly IMapper _mapper;

		private IList<Movie> Movies = new List<Movie>()
		{
			new Movie()
			{
				Id = new Guid("0c18f6ae-a16c-477e-b267-9184c4819742"),
				Name = "John Wick IV",
				Price = 15,
				Showtime = "19:00"
			},

			new Movie()
			{
				Id = new Guid("0c18f6ae-a16c-477e-b267-9184c4819800"),
				Name = "Oppenheimer",
				Price = 17,
				Showtime = "21:00"
			}
		};

		public MovieServiceTest()
		{
			_movieRepository = new Mock<IMovieRepository>();

			_movie = new Movie();

			var mockMapper = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile(new AutoMapperProfile());
			});
			var mapper = mockMapper.CreateMapper();
			_mapper = mapper;

			_movieService = new MovieService(_movieRepository.Object, _mapper);
		}

		[Fact]
		public async Task Movie_GetAll_Test()
		{
			_movieRepository.Setup(m => m.GetAll()).Returns(async () => Movies.AsEnumerable());

			var result = await _movieService.GetAll();

			var movies = result.ToList();

			movies.Should().HaveCount(2);
		}

		[Fact]
		public async Task Movie_GetById_Test()
		{
			var movieId = new Guid("0c18f6ae-a16c-477e-b267-9184c4819742");

			var expectedMovie = Movies.First(m => m.Id == movieId);

			_movieRepository.Setup(s => s.GetById(_movie.Id)).Returns(async () => Movies.FirstOrDefault(m => m.Id == movieId));

			var getMovieById = new GetByIdMovieRequest();

			var result = await _movieService.GetById(getMovieById);

			result.Should().Be(expectedMovie);
		}

		[Fact]
		public async Task Movie_Add_Test()
		{
			var movieToAdd = new Movie()
			{
				Id = new Guid("1c18f6ae-a16c-477e-b267-9184c3586742"),
				Name = "End Game",
				Price = 17,
				Showtime = "22:30"
			};

			var movieToAddRequest = new AddMovieRequest()
			{
				Name = "End Game",
				Price = 17,
				Showtime = "22:30"
			};

			_movieRepository.Setup(x => x.Add(It.IsAny<Movie>())).Callback(() =>
			Movies.Add(movieToAdd)).Returns(Task.CompletedTask);

			await _movieService.Add(movieToAddRequest);

			Movies.Should().HaveCount(3);
			Movies.Should().Contain(movieToAdd);
		}

		[Fact]
		public async Task Movie_Delete_Test()
		{
			var movieId = new Guid("0c18f6ae-a16c-477e-b267-9184c4819800");

			var movie = new DeleteMovieRequest() { Id = movieId };

			var movieToDelete = Movies.First(m => m.Id == movieId);

			_movieRepository.Setup(d => d.Delete(movie.Id)).Returns(async () => Movies.Remove(movieToDelete));

			await _movieService.Delete(movie);

			Movies.Should().HaveCount(1);
			Movies.Should().NotContain(movieToDelete);
		}
	}
}
