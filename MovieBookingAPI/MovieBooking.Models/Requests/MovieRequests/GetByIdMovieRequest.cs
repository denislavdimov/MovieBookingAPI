using System.Text.Json.Serialization;

namespace MovieBooking.Models.Requests.MovieRequests
{
	public class GetByIdMovieRequest
	{
        public Guid Id { get; set; }	
    }
}
