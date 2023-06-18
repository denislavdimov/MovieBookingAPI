namespace MovieBooking.Models.Requests.MovieRequests
{
	public class GetByIdMovieRequest
	{
        public Guid Id { get; set; }
		public string Name { get; set; }	
    }
}
