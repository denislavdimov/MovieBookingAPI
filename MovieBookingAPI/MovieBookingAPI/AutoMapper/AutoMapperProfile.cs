using AutoMapper;
using MovieBooking.Models.Models;
using MovieBooking.Models.Requests.MovieRequests;

namespace MovieBookingAPI.AutoMapper
{
	public class AutoMapperProfile : Profile
	{
        public AutoMapperProfile()
        {
            CreateMap<AddMovieRequest, Movie>();
            CreateMap<DeleteMovieRequest, Movie>();
            CreateMap<GetByIdMovieRequest, Movie>();
        }
    }
}
