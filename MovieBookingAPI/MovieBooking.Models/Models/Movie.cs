namespace MovieBooking.Models.Models
{
	public class Movie
	{
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Showtime { get; set; }
    }
}
